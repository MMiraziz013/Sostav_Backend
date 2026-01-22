using Sostav.Domain.Common;
using Sostav.Domain.Enums;

namespace Sostav.Application.Features.Venue.Dtos;

public class UpdateVenueDto
{

    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public decimal? PricePerHour { get; set; }
    public int? MinPlayers { get; set; }
    public int? MaxPlayers { get; set; }
    public string? ContactPhone { get; set; }
    public SportType? PrimarySportType { get; set; }
    public bool? HasParking { get; set; }
    public bool? HasChangingRooms { get; set; }
    public bool? HasLighting { get; set; }
    public bool? HasShowers { get; set; }

    // TODO: Think if the separate service method is needed for owner change
    // public Guid? OwnerId { get; set; }
}