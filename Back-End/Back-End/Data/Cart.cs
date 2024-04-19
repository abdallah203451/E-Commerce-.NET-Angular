using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Back_End.Data
{
	public class Cart
	{
        public int Id { get; set; }
        public int quantity { get; set; }

        public string UserId { get; set; }
		[ValidateNever]
		public User User { get; set; }

		public int ProductId { get; set; }
		[ValidateNever]
		public Product Product { get; set; }
	}
}
