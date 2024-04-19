using Back_End.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace Back_End.Contracts
{
	public interface IAuthManager
	{
		Task<IEnumerable<IdentityError>> Register(UserDto userDto);
		Task<AuthResponseDto> Login(LoginDto loginDto);
		Task<string> CreateRefreshToken();
		Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
	}
}
