using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End.Application.Orders
{
	public class OrderItemDto
	{
		public int Id { get; set; }
		public int Quantity { get; set; }
		public int ProductId { get; set; }
	}
}
