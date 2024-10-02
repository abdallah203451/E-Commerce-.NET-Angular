using AutoMapper;
using Back_End.Application.Carts;
using Back_End.Application.Repositories;
using Back_End.Domain.Entities;
using Back_End.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End.Infrastructure.Repositories
{
	public class CartRepository : ICartRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public CartRepository(ApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<IEnumerable<CartItemDto>> GetCartItemsAsync(string userId)
		{
			var cart = await _context.Carts
									 .Include(c => c.CartItems)
									 .ThenInclude(ci => ci.Product)
									 .FirstOrDefaultAsync(c => c.UserId == userId);

			if (cart == null)
			{
				return Enumerable.Empty<CartItemDto>();
			}

			return _mapper.Map<IEnumerable<CartItemDto>>(cart.CartItems);
		}

		public async Task AddCartItemAsync(int productId, string userId)
		{
			var cart = await _context.Carts
									 .Include(c => c.CartItems)
									 .FirstOrDefaultAsync(c => c.UserId == userId);

			if (cart == null)
			{
				cart = new Cart
				{
					UserId = userId,
					CartItems = new List<CartItem>()
				};
				_context.Carts.Add(cart);
			}

			var cartItem = new CartItem
			{
				ProductId = productId,  // Assuming Id is ProductId in the DTO
				Quantity = 1
			};

			cart.CartItems.Add(cartItem);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateCartItemAsync(int cartItemId, int quantity)
		{
			var cartItem = await _context.CartItems.FindAsync(cartItemId);
			if (cartItem == null) return;

			cartItem.Quantity += quantity;
			_context.CartItems.Update(cartItem);
			await _context.SaveChangesAsync();
		}

		public async Task RemoveCartItemAsync(int cartItemId)
		{
			var cartItem = await _context.CartItems.FindAsync(cartItemId);
			if (cartItem == null) return;

			_context.CartItems.Remove(cartItem);
			await _context.SaveChangesAsync();
		}

		public async Task ClearCartAsync(string userId)
		{
			await _context.CartItems.Where(ci => ci.Cart.UserId == userId).ExecuteDeleteAsync();
		}
	}
}
