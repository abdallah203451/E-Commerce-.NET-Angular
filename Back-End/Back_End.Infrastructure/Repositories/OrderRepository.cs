using Back_End.Application.Orders;
using Back_End.Application.Repositories;
using Back_End.Application.Users;
using Back_End.Domain.Entities;
using Back_End.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Back_End.Infrastructure.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly ApplicationDbContext _context;

		public OrderRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Order> GetOrderByIdAsync(int orderId)
		{
			return await _context.Orders
				.Include(o => o.OrderItems)
				.FirstOrDefaultAsync(o => o.Id == orderId);
		}

		public async Task CreateOrderAsync(Order order)
		{
			_context.Orders.Add(order);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateOrderStatusAsync(int orderId, string status, string paymentId = null, string transactionId = null)
		{
			var order = await _context.Orders.FindAsync(orderId);
			if (order != null)
			{
				if (!string.IsNullOrEmpty(transactionId) && !string.IsNullOrEmpty(paymentId))
				{
					order.TransactionId = transactionId;
					order.PaymentId = paymentId;
				}

				order.Status = status;
				order.PaymentStatus = status;

				_context.Orders.Update(order);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<OrderHistoryDto>> GetOrderHistory(string Id)
		{
			var user = await _context.Users.Include(o => o.Orders).FirstOrDefaultAsync(o => o.Id == Id);
			if(user == null)
				return null;

			List<OrderHistoryDto> orders = user.Orders
				.Select(o => new OrderHistoryDto
				{
					Id = o.Id,
					Date = o.Date,
					TotalPrice = o.TotalPrice,
					Status = o.Status,
					TransactionId = o.TransactionId
				})
				.ToList();

			if (orders == null)
				return null;

			return orders;

		}
	}
}
