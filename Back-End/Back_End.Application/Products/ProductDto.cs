using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End.Application.Products
{
	public class ProductDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public float Price { get; set; }
		public string Image { get; set; }
    }
}
