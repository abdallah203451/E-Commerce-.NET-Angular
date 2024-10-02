using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End.Application.Carts
{
	public class CartItemDto
	{
		public int Id { get; set; }
		public int Quantity { get; set; }
		public int ProductId { get; set; }
		public float Price { get; set; }
		public float Discount { get; set; }
		public string Name { get; set; }
		public string Image { get; set; }
	}
}
