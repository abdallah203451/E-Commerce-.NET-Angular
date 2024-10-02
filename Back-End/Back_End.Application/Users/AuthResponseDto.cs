using System.ComponentModel.DataAnnotations;

namespace Back_End.Application.Users
{
	public class AuthResponseDto
	{
		public string UserId { get; set; }
		public string Token { get; set; }
		public string RefreshToken { get; set; }
        public string Role { get; set; }
    }
}
