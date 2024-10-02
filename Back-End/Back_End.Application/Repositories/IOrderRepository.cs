using Back_End.Application.Orders;
using Back_End.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End.Application.Repositories
{
	public interface IOrderRepository
	{
		Task<Order> GetOrderByIdAsync(int orderId);
		Task CreateOrderAsync(Order order);
		Task UpdateOrderStatusAsync(int orderId, string status, string paymentId = null, string transactionId = null);
		Task<List<OrderHistoryDto>> GetOrderHistory(string Id);
	}
}
