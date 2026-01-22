using Sostav.Domain.Common;
using Sostav.Domain.Enums;

namespace Sostav.Domain.Entities;

public class GameParticipant : BaseEntity
{
    public DateTime JoinedAt { get; set; }
    public ParticipantStatus Status { get; set; } = ParticipantStatus.Confirmed;
    
    // Foreign keys
    public Guid GameId { get; set; }
    public Guid UserId { get; set; }
    
    // Navigation properties
    public virtual Game Game { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
