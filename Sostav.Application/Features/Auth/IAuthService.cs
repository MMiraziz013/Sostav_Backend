using Sostav.Application.Common.Models;
using Sostav.Application.Features.Auth.Dtos;

namespace Sostav.Application.Features.Auth;

public interface IAuthService
{
    Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request);
    Task<Result<AuthResponse>> LoginAsync(LoginRequest request);
    Task<Result<UserDto>> GetCurrentUserAsync(Guid userId);
}
