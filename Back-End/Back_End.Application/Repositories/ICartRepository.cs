using Back_End.Application.Carts;
using Back_End.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End.Application.Repositories
{
	public interface ICartRepository
	{
		Task<IEnumerable<CartItemDto>> GetCartItemsAsync(string userId);
		Task AddCartItemAsync(int productId, string userId);
		Task UpdateCartItemAsync(int cartItemId, int quantity);
		Task RemoveCartItemAsync(int cartItemId);
		Task ClearCartAsync(string userId);
	}
}
