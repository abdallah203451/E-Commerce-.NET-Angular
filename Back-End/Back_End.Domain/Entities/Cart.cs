using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End.Domain.Entities
{
	public class Cart
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		[ValidateNever]
		public User User { get; set; }
		public List<CartItem> CartItems { get; set; } = new List<CartItem>();
	}
}
