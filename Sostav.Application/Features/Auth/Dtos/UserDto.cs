namespace Sostav.Application.Features.Auth.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? TelegramUsername { get; set; }
    public string? AvatarUrl { get; set; }
    public string City { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    
    // Stats (can be populated separately)
    public int GamesPlayed { get; set; }
    public int GamesOrganized { get; set; }
}
