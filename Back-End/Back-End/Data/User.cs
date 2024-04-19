using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Back_End.Data
{
	public class User : IdentityUser
	{
		public string FullName { get; set; }
		public string Phone { get; set; }
		[ValidateNever]
		public List<Product> UserAddedProducts { get; set; }
		[ValidateNever]
		public List<Cart> Carts { get; set; }

    }
}
