using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End.Application.Payments
{
	public class PaymentExecutionResultDto
	{
		public bool Success { get; set; }
		public string TransactionId { get; set; }
	}
}
