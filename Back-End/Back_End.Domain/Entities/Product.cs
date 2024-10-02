using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Back_End.Domain.Entities
{
	public class Product
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public string Image { get; set; }

        public int CategoryId { get; set; }
		[ValidateNever]
		public Category Category { get; set; }

        public string UserId { get; set; }
        [ValidateNever]
        public User User { get; set; }

        public DateTime AddDate { get; set; }

		[ValidateNever]
		public List<OrderItem> OrderItems { get; set; }

    }
}

