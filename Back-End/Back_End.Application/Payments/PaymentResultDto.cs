using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End.Application.Payments
{
	public class PaymentResultDto
	{
		public bool Success { get; set; }
		public string PaymentUrl { get; set; }
		public string PaymentId { get; set; }
	}
}
