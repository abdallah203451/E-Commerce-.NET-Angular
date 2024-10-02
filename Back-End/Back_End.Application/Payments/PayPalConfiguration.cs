using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End.Application.Payments
{
	public class PayPalConfiguration
	{
		public static APIContext GetAPIContext()
		{
			// Replace these with your PayPal sandbox credentials
			var clientId = "AY54YTCgQfeARQrr08nIBY3cT9fzvS11PNrf4QA0RVXuU4_ubSxBsGPKwvp6CkvIidTWR1clAtZFWaKo";
			var clientSecret = "EMUDQqqOcEIGELqQjELjTfBTivfMe5BuoSwMfj4RdyTmMMWujFA219f9H8i9mte0akgNkEZimH9bED6q";

			var config = new Dictionary<string, string>
		{
			{ "mode", "sandbox" },  // Switch to "live" in production
            { "clientId", clientId },
			{ "clientSecret", clientSecret }
		};

			var accessToken = new OAuthTokenCredential(config).GetAccessToken();
			return new APIContext(accessToken);
		}
	}
}
