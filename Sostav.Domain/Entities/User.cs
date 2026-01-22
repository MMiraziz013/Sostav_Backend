using Sostav.Domain.Common;
using Sostav.Domain.Enums;

namespace Sostav.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? TelegramUsername { get; set; }
    public string? AvatarUrl { get; set; }
    public string City { get; set; } = "Tashkent";
    public UserRole Role { get; set; } = UserRole.Player;
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public virtual ICollection<Venue> OwnedVenues { get; set; } = new List<Venue>();
    public virtual ICollection<Game> OrganizedGames { get; set; } = new List<Game>();
    public virtual ICollection<GameParticipant> GameParticipations { get; set; } = new List<GameParticipant>();
}
