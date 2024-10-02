using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End.Application.Products
{
	public class ProductFilterDto
	{
        public int Category { get; set; }
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }
        public string? Name { get; set; }
        public int PageNumber { get; set; }
    }
}
