using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Back_End.Domain.Entities
{
	public class Order
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public float TotalPrice { get; set; }
		public string Status { get; set; } // Order status (e.g., Pending, Completed, Failed)
		public string? TransactionId { get; set; } // PayPal Transaction ID
		public string? PaymentId { get; set; } // PayPal Payment ID (for tracking purposes)
		//public string PayerId { get; set; } // PayPal Payer ID (if needed)
		public string? PaymentStatus { get; set; } // Status of the payment (e.g., PaymentInitiated, PaymentCompleted)

		public string UserId { get; set; }
		[ValidateNever]
		public User User { get; set; }

		public List<OrderItem> OrderItems { get; set; }

	}
}
