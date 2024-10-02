using Back_End.Application.Carts;
using Back_End.Application.Repositories;
using Back_End.Domain.Entities;
using Back_End.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Back_End.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class CartController : ControllerBase
	{
		private readonly ICartRepository _cartRepository;

		public CartController(ICartRepository cartRepository)
		{
			_cartRepository = cartRepository;
		}

		[HttpGet("GetCartItems")]
		public async Task<ActionResult<IEnumerable<CartItemDto>>> GetCartItems()
		{
			var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
			var cartItems = await _cartRepository.GetCartItemsAsync(userId);
			return Ok(cartItems);
		}

		[HttpPost("AddCartItem")]
		public async Task<IActionResult> AddCartItem([FromBody] int productId)
		{
			var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
			await _cartRepository.AddCartItemAsync(productId, userId);
			return Ok();
		}

		[HttpPut("UpdateCartItem/{cartItemId}")]
		public async Task<IActionResult> UpdateCartItem(int cartItemId, [FromBody] int quantity)
		{
			await _cartRepository.UpdateCartItemAsync(cartItemId, quantity);
			return Ok();
		}

		[HttpDelete("RemoveCartItem/{cartItemId}")]
		public async Task<IActionResult> RemoveCartItem(int cartItemId)
		{
			await _cartRepository.RemoveCartItemAsync(cartItemId);
			return Ok();
		}

		[HttpDelete("ClearCart")]
		public async Task<IActionResult> ClearCart()
		{
			var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
			await _cartRepository.ClearCartAsync(userId);
			return Ok();
		}
	}
}
