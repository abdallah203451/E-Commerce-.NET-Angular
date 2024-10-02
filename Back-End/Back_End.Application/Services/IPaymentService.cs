using Back_End.Application.Payments;
using Back_End.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End.Application.Services
{
    public interface IPaymentService
    {
		Task<PaymentResultDto> CreateOrderAsync(float amount, string currency);
		Task<PaymentExecutionResultDto> ExecutePaymentAsync(string paymentId, string payerId);
	}
}
