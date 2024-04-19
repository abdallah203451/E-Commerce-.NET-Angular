using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Back_End.Data
{
	public class Product
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public string Image { get; set; }

        public int CategoryId { get; set; }
		[ValidateNever]
		public Category Category { get; set; }

        public string UserIdAddedBy { get; set; }
        [ValidateNever]
        public User User { get; set; }

		[ValidateNever]
		public List<Cart> Carts { get; set; }

		public DateTime AddDate { get; set; }
    }
}
