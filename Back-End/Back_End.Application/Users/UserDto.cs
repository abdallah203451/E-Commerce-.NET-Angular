﻿using System.ComponentModel.DataAnnotations;

namespace Back_End.Application.Users
{
	public class UserDto
	{
		[Required]
		public string FullName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Phone { get; set; }

		[Required]
		[StringLength(15, ErrorMessage ="Your Password is limited to {2} to {1} characters", MinimumLength =8)]
		public string Password { get; set; }
        public bool IsSeller { get; set; }
    }
}
