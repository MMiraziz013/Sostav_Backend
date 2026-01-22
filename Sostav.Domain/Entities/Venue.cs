using Sostav.Domain.Common;
using Sostav.Domain.Enums;

namespace Sostav.Domain.Entities;

public class Venue : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Description { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public decimal PricePerHour { get; set; }
    public int MinPlayers { get; set; }
    public int MaxPlayers { get; set; }
    public string ContactPhone { get; set; } = string.Empty;
    public SportType PrimarySportType { get; set; }
    public bool HasParking { get; set; }
    public bool HasChangingRooms { get; set; }
    public bool HasLighting { get; set; }
    public bool HasShowers { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Foreign keys
    public Guid? OwnerId { get; set; }
    
    // Navigation properties
    public virtual User? Owner { get; set; }
    public virtual ICollection<VenuePhoto> Photos { get; set; } = new List<VenuePhoto>();
    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
