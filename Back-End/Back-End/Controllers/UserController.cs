using Back_End.Contracts;
using Back_End.Data;
using Back_End.Models.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Back_End.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AllowAnonymous]
	public class UserController : ControllerBase
	{
		private readonly IAuthManager _authManager;
		private readonly IConfiguration _configuration;
		private readonly UserManager<User> _userManager;

		public UserController(IAuthManager authManager, IConfiguration configuration, UserManager<User> userManager) 
		{
			_authManager = authManager;
			_configuration = configuration;
			_userManager = userManager;
		}


		[HttpPost("login")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
		{
			if (User.Identity.IsAuthenticated)
			{
				var x = 1;
			}
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

	}
}
