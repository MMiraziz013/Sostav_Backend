using Sostav.Domain.Common;
using Sostav.Domain.Enums;

namespace Sostav.Domain.Entities;

public class Game : BaseEntity
{
    public SportType SportType { get; set; }
    public DateTime ScheduledAt { get; set; }
    public int DurationMinutes { get; set; } = 60;
    public int MaxPlayers { get; set; }
    public decimal PricePerPlayer { get; set; }
    public string? Description { get; set; }
    public GameStatus Status { get; set; } = GameStatus.Open;
    public bool IsPublic { get; set; } = true;
    
    // Foreign keys
    public Guid VenueId { get; set; }
    public Guid OrganizerId { get; set; }
    
    // Navigation properties
    public virtual Venue Venue { get; set; } = null!;
    public virtual User Organizer { get; set; } = null!;
    public virtual ICollection<GameParticipant> Participants { get; set; } = new List<GameParticipant>();
    
    // Computed property (not mapped to DB)
    public int CurrentPlayerCount => Participants.Count(p => p.Status == ParticipantStatus.Confirmed);
    public int SpotsAvailable => MaxPlayers - CurrentPlayerCount;
    public bool IsFull => CurrentPlayerCount >= MaxPlayers;
}
