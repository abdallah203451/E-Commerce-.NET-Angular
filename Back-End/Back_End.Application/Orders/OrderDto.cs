using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End.Application.Orders
{
	public class OrderDto
	{
		public int Id { get; set; }
		public float TotalPrice { get; set; }
		public List<OrderItemDto> OrderItems { get; set; }
	}
}
