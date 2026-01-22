using Sostav.Domain.Common;

namespace Sostav.Domain.Entities;

public class VenuePhoto : BaseEntity
{
    public string Url { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }
    public int DisplayOrder { get; set; }
    
    // Foreign keys
    public Guid VenueId { get; set; }
    
    // Navigation properties
    public virtual Venue Venue { get; set; } = null!;
}
