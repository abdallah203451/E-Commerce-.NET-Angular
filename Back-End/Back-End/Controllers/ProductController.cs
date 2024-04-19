using Back_End.Contracts;
using Back_End.Data;
using Back_End.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public ProductController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost("addproduct")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[Authorize(Roles = "Administrator")]
		public async Task<ActionResult> AddProduct([FromBody] Product product)
		{
			product.AddDate = DateTime.UtcNow;
			await _context.AddAsync(product);
			await _context.SaveChangesAsync();
			return Ok(new
			{
				Message = "Product added successfully"
			});
		}

		[HttpGet("getallproducts")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[Authorize]
		public async Task<ActionResult> GetAllProducts(int categoryId)
		{
			List<Product> products;
			if (categoryId == 1)
			{
				products = await _context.Products.ToListAsync();
			}
			else
			{
				products = await _context.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
			}
			return Ok(products);
		}

	}
}
