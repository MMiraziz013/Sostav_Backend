using Sostav.Domain.Entities;

namespace Sostav.Application.Common.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}
