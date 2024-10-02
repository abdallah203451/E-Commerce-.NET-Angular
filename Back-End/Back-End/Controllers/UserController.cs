using Back_End.Application.Repositories;
using Back_End.Application.Users;
using Back_End.Domain.Entities;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Security.Claims;
using System.Web;

namespace Back_End.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AllowAnonymous]
	public class UserController : ControllerBase
	{
		private readonly IAuthManager _authManager;
		//private readonly IConfiguration _configuration;
		//private readonly UserManager<User> _userManager;
		//private readonly HttpContext _httpContext;

		public UserController(IAuthManager authManager) 
		{
			_authManager = authManager;
			//_configuration = configuration;
			//_userManager = userManager;
			//_httpContext = httpContext;
		}


		[HttpPost("login")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
		{
			//if (_httpContext.User.Claims.)
			//{
			//	var x = 1;
			//}

			var authResponse = await _authManager.Login(loginDto);

			if (authResponse == null)
			{
				return Unauthorized();
			}
			return Ok(authResponse);
		}


		[HttpPost("register")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> Register([FromBody] UserDto userDto)
		{
			var errors = await _authManager.Register(userDto);

			if(errors.Any())
			{
				foreach(var error in errors)
				{
					ModelState.AddModelError(error.Code, error.Description);
				}
				return BadRequest(ModelState);
			}

			return Ok();
		}


		[HttpPost("refreshtoken")]
		//[Route("refreshtoken")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> RefreshToken([FromBody] AuthResponseDto request)
		{
			var authResponse = await _authManager.VerifyRefreshToken(request);

			if (authResponse == null)
			{
				return Unauthorized();
			}
			return Ok(authResponse);
		}

		[HttpPost("forgot-password")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordDto email)
		{
			var token = await _authManager.GeneratePasswordResetTokenAsync(email.Email);
			if (token == null)
				return BadRequest();

			return Ok();
		}

		[HttpPost("reset-password")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
		{
			var result = await _authManager.ResetPasswordAsync(dto.Email, dto.Token, dto.NewPassword);
			if (!result)
				return BadRequest();

			return Ok();
		}

		[Authorize]
		[HttpGet("profile-data")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> ProfileData()
		{
			var email = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Email).Value;
			var data = await _authManager.GetProfileDataAsync(email);
			if(data == null) return BadRequest();
			return Ok(data);
		}

		[Authorize]
		[HttpPut("update-profile")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> UpdateProfile([FromBody] ProfileDataDto profileDataDto)
		{
			if(profileDataDto == null) return BadRequest();
			var result = await _authManager.UpdateProfileDataAsync(profileDataDto);
			if (!result) return BadRequest();

			return Ok();
		}

		[Authorize]
		[HttpPost("change-password")]
		public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto passwordDto)
		{
			// Get the current user based on the JWT token claims
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (userId == null)
			{
				return Unauthorized("User not found.");
			}

			IdentityResult result = await _authManager.ChangePasswordAsync(userId, passwordDto);

			if (!result.Succeeded)
			{
				// If password change failed, return validation errors
				var errors = result.Errors.Select(e => e.Description);
				return BadRequest(new { Errors = errors });
			}

			// If successful, return a success message
			return Ok(new { Message = "Password changed successfully." });
		}

	}
}
