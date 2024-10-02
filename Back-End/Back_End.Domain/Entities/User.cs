using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Back_End.Domain.Entities
{
	public class User : IdentityUser
	{
		public string FullName { get; set; }
		public string Phone { get; set; }

		[ValidateNever]
		public List<Order> Orders { get; set; }

		[ValidateNever]
		public List<Product> Products { get; set; }

	}
}
