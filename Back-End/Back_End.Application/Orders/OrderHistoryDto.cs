namespace Back_End.Application.Orders
{
	public class OrderHistoryDto
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public float TotalPrice { get; set; }
		public string Status { get; set; }
		public string? TransactionId { get; set; }
	}
}
