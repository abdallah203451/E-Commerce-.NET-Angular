using Back_End.Application.Users;
using Microsoft.AspNetCore.Identity;

namespace Back_End.Application.Repositories
{
	public interface IAuthManager
	{
		Task<IEnumerable<IdentityError>> Register(UserDto userDto);
		Task<AuthResponseDto> Login(LoginDto loginDto);
		Task<string> CreateRefreshToken();
		Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
		Task<string> GeneratePasswordResetTokenAsync(string email);
		Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
		Task<ProfileDataDto> GetProfileDataAsync(string email);
		Task<bool> UpdateProfileDataAsync(ProfileDataDto profileDataDto);
		Task<IdentityResult> ChangePasswordAsync(string userId, ChangePasswordDto passwordDto);
	}
}
