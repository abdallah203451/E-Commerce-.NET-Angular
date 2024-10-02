using Back_End.Application.Services;
//using Back_End.Domain.Entities;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using PayPal.Api;
using PayPalCheckoutSdk.Orders;
using PayPalCheckoutSdk.Core;
using Back_End.Application.Payments;
//using Payment = PayPal.Api.Payment;
//using Payer = PayPal.Api.Payer;


namespace Back_End.Infrastructure.Services
{
	public class PayPalPaymentService : IPaymentService
	{
		private readonly PayPalHttpClient _client;

		public PayPalPaymentService()
		{
			var environment = new SandboxEnvironment("Affuq41RK0LuTixnsg4GmlHPC_CcQG2Xs_IOPH4CjDHbJOneHispQ1kdL1mpAPCVU6DqHu_yJqu3vnj8", "EIxO7YscbDMxTGpY1961aEgnTR0g-dFzNu63cDvubeOohv9aJEKMcrKVptcteRRHroXaxgdi4WyOGX_R");
			_client = new PayPalHttpClient(environment);
		}

		public async Task<PaymentResultDto> CreateOrderAsync(float amount, string currency)
		{
			var order = new OrderRequest()
			{
				CheckoutPaymentIntent = "CAPTURE",
				PurchaseUnits = new List<PurchaseUnitRequest>
				{
					new PurchaseUnitRequest
					{
						AmountWithBreakdown = new AmountWithBreakdown
						{
							CurrencyCode = currency,
							Value = amount.ToString()
						}
					}
				},
				ApplicationContext = new ApplicationContext
				{
					ReturnUrl = "http://localhost:4200/payment",
					CancelUrl = "http://localhost:4200/payment"
				}
			};

			var request = new OrdersCreateRequest();
			request.Prefer("return=representation");
			request.RequestBody(order);

			var response = await _client.Execute(request);
			var result = response.Result<Order>();

			var approvalLink = result.Links.Find(link => link.Rel.Equals("approve", StringComparison.OrdinalIgnoreCase))?.Href;

			return new PaymentResultDto
			{
				Success = !string.IsNullOrEmpty(approvalLink),
				PaymentUrl = approvalLink,
				PaymentId = result.Id
			};
			
		}

		public async Task<PaymentExecutionResultDto> ExecutePaymentAsync(string paymentId, string payerId)
		{
			var request = new OrdersCaptureRequest(paymentId);
			request.RequestBody(new OrderActionRequest());

			var response = await _client.Execute(request);
			var result = response.Result<Order>();

			// Check if the payment was completed successfully
			var captureId = result.PurchaseUnits[0].Payments.Captures[0].Id;

			return new PaymentExecutionResultDto
			{
				Success = result.Status == "COMPLETED",
				TransactionId = captureId
			};
		}
		//public async Task<PaymentResultDto> CreateOrderAsync(float amount, string currency)
		//{
		//	var apiContext = PayPalConfiguration.GetAPIContext();

		//	var payment = new Payment
		//	{
		//		intent = "sale",
		//		payer = new Payer { payment_method = "paypal" },
		//		transactions = new List<Transaction>
		//	{
		//		new Transaction
		//		{
		//			amount = new Amount { currency = currency, total = amount.ToString() },
		//			description = "Payment for order"
		//		}
		//	},
		//		redirect_urls = new RedirectUrls
		//		{
		//			return_url = "http://localhost:4200/cart",
		//			cancel_url = "http://localhost:4200/cart/payment-cancel"
		//		}
		//	};

		//	var createdPayment = payment.Create(apiContext);

		//	var approvalUrl = createdPayment.links.FirstOrDefault(link => link.rel == "approval_url")?.href;

		//	return new PaymentResultDto
		//	{
		//		Success = !string.IsNullOrEmpty(approvalUrl),
		//		PaymentUrl = approvalUrl,
		//		PaymentId = createdPayment.id
		//	};
		//}

		//public async Task<PaymentExecutionResultDto> ExecutePaymentAsync(string paymentId, string payerId)
		//{
		//	var apiContext = PayPalConfiguration.GetAPIContext();

		//	var paymentExecution = new PaymentExecution { payer_id = payerId };
		//	var payment = new Payment { id = paymentId };

		//	var executedPayment = payment.Execute(apiContext, paymentExecution);

		//	return new PaymentExecutionResultDto
		//	{
		//		Success = executedPayment.state.ToLower() == "approved",
		//		TransactionId = executedPayment.transactions.FirstOrDefault()?.related_resources.FirstOrDefault()?.sale?.id
		//	};
		//}
	}
}
