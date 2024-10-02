using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Back_End.Infrastructure.Services
{
	public class EmailSender : IEmailSender
	{
		private readonly AuthMessageSenderOptions _options;

		public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
		{
			_options = optionsAccessor.Value;
		}

		public Task SendEmailAsync(string email, string subject, string message)
		{
			return Execute(_options.SendGridKey, subject, message, email);
		}

		private Task Execute(string apiKey, string subject, string message, string email)
		{
			var client = new SendGridClient(apiKey);
			var msg = new SendGridMessage()
			{
				From = new EmailAddress("abdallahashraf.307@gmail.com", "ECommerce"),
				Subject = subject,
				PlainTextContent = message,
				HtmlContent = message
			};
			msg.AddTo(new EmailAddress(email));

			return client.SendEmailAsync(msg);
		}
	}
}
