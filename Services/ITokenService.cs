using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services;

public interface ITokenService
{
    (string Token, DateTime ExpiresAt) GenerateToken(User user);
}