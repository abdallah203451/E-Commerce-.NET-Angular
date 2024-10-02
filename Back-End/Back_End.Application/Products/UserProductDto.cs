using Back_End.Domain.Entities;

namespace Back_End.Application.Products
{
	public class UserProductDto
	{
		public Product Product { get; set; }
		public int QuantitySold { get; set; }
	}
}
