using Back_End.Domain.Entities;
using Back_End.Infrastructure.Context;
using Back_End.Infrastructure.Repositories;
using Back_End.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Back_End.Application.Services;
using Back_End.Application.Products;

namespace Back_End.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductRepository _productRepository;
		private readonly ITrendingProductsCacheService _productService;
		public ProductController(IProductRepository productRepository, ITrendingProductsCacheService productService)
		{
			_productRepository = productRepository;
			_productService = productService;
		}

		[HttpPost("addproduct")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[Authorize(Roles = "Administrator")]
		public async Task<ActionResult> AddProduct([FromBody] Product product)
		{
			bool result = await _productRepository.AddProduct(product);
			if (result)
			{
				return Ok(new
				{
					Message = "Product added successfully"
				});
			}
			return BadRequest("Adding product failed.");
		}

		[HttpGet("getallproducts")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> GetAllProducts([FromQuery] ProductFilterDto filterDto)
		{
			//if (true)
			//{
			//	var iden = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
			//}
			PagedResult<Product> products = await _productRepository.GetAllProducts(filterDto, 8);

			return Ok(products);
		}

		[HttpGet("gettrendingproducts")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> GetTrendingProducts([FromQuery] ProductFilterDto filterDto)
		{
			PagedResult<ProductDto> products = await _productService.GetTrendingProductsAsync(filterDto, 8);

			return Ok(products);
		}

		[HttpGet("getproductdetails")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[Authorize]
		public async Task<ActionResult> GetProductDetails([FromQuery] int Id)
		{
			Product product = await _productRepository.GetProductDetails(Id);

			return Ok(product);
		}

		[HttpGet("user-products")]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> GetUserProducts()
		{
			// Get the user ID from the claims
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (string.IsNullOrEmpty(userId))
			{
				return Unauthorized("User is not authenticated.");
			}

			// Fetch the user's products and sold quantities
			var userProducts = await _productRepository.GetUserProductsAsync(userId);

			return Ok(userProducts);
		}

		[HttpDelete("delete-product")]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var result = await _productRepository.DeleteProductAsync(id);

			if (!result)
			{
				return BadRequest("Deleting product failed.");
			}

			return Ok();
		}

		[HttpPut("edit-product")]
		[Authorize(Roles = "Administrator")]
		public async Task<ActionResult> EditProduct([FromBody] Product product)
		{
			bool result = await _productRepository.EditProduct(product);
			if (result)
			{
				return Ok(new
				{
					Message = "Product edited successfully"
				});
			}
			return BadRequest("Editing product failed.");
		}
	}
}
 