namespace Back_End.Application.Products
{
	public class TrendingProductFilter
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public float Price { get; set; }
		public string Image { get; set; }
		public int CategoryId { get; set; }
		public int TotalQuantitySold { get; set; }
	}
}
