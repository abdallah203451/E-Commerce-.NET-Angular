using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Back_End.Domain.Entities
{
	public class Category
	{
        public int Id { get; set; }
        public string Name { get; set; }
		[ValidateNever]
		public List<Product> Products { get; set; }
    }
}
