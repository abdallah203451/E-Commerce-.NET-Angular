using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Back_End.Domain.Entities
{
	public class OrderItem
	{
        public int Id { get; set; }
        public int quantity { get; set; }

        public float Price { get; set; }

        public int ProductId { get; set; }
		[ValidateNever]
		public Product Product { get; set; }

        public int OrderId { get; set; }
        [ValidateNever]
        public Order Order { get; set; }
    }
}
