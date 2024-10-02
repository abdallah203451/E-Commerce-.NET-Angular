using Back_End.Application.Carts;
using Back_End.Application.Orders;
using Back_End.Application.Repositories;
using Back_End.Application.Services;
using Back_End.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Back_End.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IPaymentService _payPalService;

		public OrderController(IOrderRepository orderRepository, IPaymentService payPalService)
		{
			_orderRepository = orderRepository;
			_payPalService = payPalService;
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateOrderAndInitiatePayment([FromBody] List<CartItemDto> cartItems)
		{
			var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

			// Step 1: Create the Order
			var order = new Order
			{
				Date = DateTime.Now,
				TotalPrice = cartItems.Sum(x => (x.Price * x.Quantity) - ((x.Discount/100) * (x.Price * x.Quantity))),
				Status = "Pending",
				UserId = userId,
				OrderItems = cartItems.Select(x => new OrderItem
				{
					ProductId = x.ProductId,
					quantity = x.Quantity,
					Price = x.Price,
				}).ToList()
			};

			await _orderRepository.CreateOrderAsync(order);

			// Step 2: Initiate PayPal Payment
			var paymentResult = await _payPalService.CreateOrderAsync(order.TotalPrice, "USD");

			if (paymentResult.Success)
			{
				order.Status = "Payment Initiated";
				await _orderRepository.UpdateOrderStatusAsync(order.Id, order.Status);

				return Ok(new { orderId = order.Id, paymentUrl = paymentResult.PaymentUrl });
			}

			return BadRequest("Failed to initiate payment.");
		}

		[HttpGet("payment")]
		public async Task<IActionResult> ConfirmPayment([FromQuery] string paymentId, [FromQuery] string payerId, [FromQuery] int orderId)
		{
			var result = await _payPalService.ExecutePaymentAsync(paymentId, payerId);

			if (result.Success)
			{
				await _orderRepository.UpdateOrderStatusAsync(orderId, "Completed", result.TransactionId, paymentId);
				return Ok();
			}

			await _orderRepository.UpdateOrderStatusAsync(orderId, "Failed");
			return BadRequest("Payment failed.");
		}

		[HttpGet("get-orders")]
		public async Task<IActionResult> GetOrdersHistory()
		{
			var Id = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
			List<OrderHistoryDto> orders = await _orderRepository.GetOrderHistory(Id);

			if (orders == null) return BadRequest();

			return Ok(orders);
			
		}
	}
}
