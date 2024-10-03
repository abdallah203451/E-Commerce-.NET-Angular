using AutoMapper;
using Back_End.Application.Repositories;
using Back_End.Application.Users;
using Back_End.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace Back_End.Infrastructure.Repositories
{
	public class AuthManager : IAuthManager
	{
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;
		private readonly IEmailSender _emailSender;
		private readonly IConfiguration _configuration;
		private User _user;

		private const string _loginProvider = "Back-End";
		private const string _refreshToken = "RefreshToken";

		public AuthManager(IMapper mapper, UserManager<User> userManager, IConfiguration configuration, IEmailSender emailSender) 
		{
			_mapper = mapper;
			_userManager = userManager;
			_configuration = configuration;
			_emailSender = emailSender;
		}

		public async Task<IEnumerable<IdentityError>> Register(UserDto userDto)
		{
			_user = _mapper.Map<User>(userDto);
			_user.UserName = userDto.Email;

			var result = await _userManager.CreateAsync(_user, userDto.Password);

			if (result.Succeeded)
			{
				if(userDto.IsSeller == false)
					await _userManager.AddToRoleAsync(_user, "User");
				else
					await _userManager.AddToRoleAsync(_user, "Administrator");
			}

			return result.Errors;
		}

		public async Task<AuthResponseDto> Login(LoginDto loginDto)
		{
			_user = await _userManager.FindByEmailAsync(loginDto.Email);
			bool isValidUser = await _userManager.CheckPasswordAsync(_user, loginDto.Password);

			if(_user == null || isValidUser == false)
			{
				return null;
			}

			var token = await GenerateToken();

			IList<string> roles = await _userManager.GetRolesAsync(_user);
			string role = roles[0];


			return new AuthResponseDto
			{
				Token = token,
				UserId = _user.Id,
				RefreshToken = await CreateRefreshToken(),
				Role = role
			};

		}

		private async Task<string> GenerateToken()
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var roles = await _userManager.GetRolesAsync(_user);
			var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
			var userCalims = await _userManager.GetClaimsAsync(_user);

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, _user.Id),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email, _user.Email),
				new Claim("uid", _user.Id),
			}
			.Union(userCalims).Union(roleClaims);

			var token = new JwtSecurityToken(
				issuer: _configuration["JwtSettings:Issuer"],
				audience: _configuration["JwtSettings:Audience"],
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
				signingCredentials: credentials
				);
			var t = new JwtSecurityTokenHandler().WriteToken(token);
			return t;
		}

		public async Task<string> CreateRefreshToken()
		{
			await _userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);
			var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);
			var result = await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);

			return newRefreshToken;
		}

		public async Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request)
		{
			var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
			var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
			var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;
			_user = await _userManager.FindByNameAsync(username);
			
			if (_user == null || _user.Id != request.UserId)
			{
				return null;
			}

			var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, request.RefreshToken);

			if(isValidRefreshToken)
			{
				var token = await GenerateToken();

				return new AuthResponseDto
				{
					Token = token,
					UserId = _user.Id,
					RefreshToken = await CreateRefreshToken()
				};
			}

			await _userManager.UpdateSecurityStampAsync(_user);
			return null;
		}

		public async Task<string> GeneratePasswordResetTokenAsync(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null) return null;

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);

			// Send the token via email
			var resetLink = $"http://localhost:4200/reset-password?token={HttpUtility.UrlEncode(token)}&email={email}";


			// Load email template from a file
			string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "PasswordResetEmailTemplate.html");
			string emailBody = await File.ReadAllTextAsync(templatePath);

			// Replace the placeholder with the actual reset link
			emailBody = emailBody.Replace("{{ResetLink}}", resetLink);


			await _emailSender.SendEmailAsync(email, "Reset Your Password", emailBody);

			return token;
		}

		public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null) return false;

			var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

			return result.Succeeded;
		}

		public async Task<ProfileDataDto> GetProfileDataAsync(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);

			if(user == null) return null;
			ProfileDataDto data = _mapper.Map<ProfileDataDto>(user);
			//ProfileDataDto dataDto = new ProfileDataDto();
			//dataDto.Email = user.Email;
			//dataDto.FullName = user.FullName;
			//dataDto.Phone = user.Phone;

			return data;
		}

		public async Task<bool> UpdateProfileDataAsync(ProfileDataDto profileDataDto)
		{
			var user = await _userManager.FindByEmailAsync(profileDataDto.Email);

			if (user == null) return false;
			user.FullName = profileDataDto.FullName;
			user.Phone = profileDataDto.Phone;
			var result = await _userManager.UpdateAsync(user);
			return result.Succeeded;
		}

		public async Task<IdentityResult> ChangePasswordAsync(string userId, ChangePasswordDto passwordDto)
		{
			var user = await _userManager.FindByIdAsync(userId);

			IdentityResult changePasswordResult = await _userManager.ChangePasswordAsync(user, passwordDto.OldPassword, passwordDto.NewPassword);
			return changePasswordResult;
		}
	}
}
