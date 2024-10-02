using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End.Application.Users
{
	public class ResetPasswordDto
	{
		public string Email { get; set; }
		public string Token { get; set; }
		public string NewPassword { get; set; }
	}
}
