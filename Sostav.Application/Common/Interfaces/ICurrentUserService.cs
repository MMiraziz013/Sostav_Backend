namespace Sostav.Application.Common.Interfaces;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    string? Phone { get; }
    string? Role { get; }
    bool IsAuthenticated { get; }
}
