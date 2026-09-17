using ExpenseTrackerAPI.DTOs.Auth;

namespace ExpenseTrackerAPI.Services;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
}