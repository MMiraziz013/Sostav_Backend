using Sostav.Application.Common.Interfaces;
using Sostav.Application.Common.Models;
using Sostav.Application.Features.Venue.Dtos;
using Sostav.Domain.Enums;

namespace Sostav.Application.Features.Venue;

public class VenueService : IVenueService
{
    private readonly IVenueRepository _venueRepository;

    public VenueService(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }


    public async Task<Result<GetVenueMinimalDto>> AddVenueAsync(AddVenueDto dto)
    {
        var venueToAdd = new Domain.Entities.Venue
        {
            Name = dto.Name,
            Address = dto.Address,
            Description = dto.Description,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            PricePerHour = dto.PricePerHour,
            MinPlayers = dto.MinPlayers,
            MaxPlayers = dto.MaxPlayers,
            ContactPhone = dto.ContactPhone,
            PrimarySportType = dto.PrimarySportType,
            HasParking = dto.HasParking,
            HasChangingRooms = dto.HasChangingRooms,
            HasLighting = dto.HasLighting,
            HasShowers = dto.HasShowers,
            IsActive = true,
            OwnerId = dto.OwnerId,
        };

        var isAdded = await _venueRepository.AddVenueAsync(venueToAdd);
        if (isAdded is null)
        {
            return Result.Failure<GetVenueMinimalDto>("Error while adding the venue");
        }

        var returning = new GetVenueMinimalDto
        {
            Id = isAdded.Id,
            Name = isAdded.Name,
            Address = isAdded.Address,
            Description = isAdded.Description,
            Latitude = isAdded.Latitude,
            Longitude = isAdded.Longitude,
            PricePerHour = isAdded.PricePerHour,
            MinPlayers = isAdded.MinPlayers,
            MaxPlayers = isAdded.MaxPlayers,
            ContactPhone = isAdded.ContactPhone,
            PrimarySportType = SportType.Football,
            OwnerName = isAdded.Owner?.FullName ?? "Unknown"
        };

        return returning;
    }

    public async Task<Result<List<GetVenueDto>>> GetVenuesFullInfoAsync()
    {
        var venues = await _venueRepository.GetVenuesFullInfoAsync();
        var data = venues.Select(v => new GetVenueDto
        {
            Id = v.Id,
            Name = v.Name,
            Address = v.Address,
            Description = v.Description,
            Latitude = v.Latitude,
            Longitude = v.Longitude,
            PricePerHour = v.PricePerHour,
            MinPlayers = v.MinPlayers,
            MaxPlayers = v.MaxPlayers,
            ContactPhone = v.ContactPhone,
            PrimarySportType = v.PrimarySportType,
            HasParking = v.HasParking,
            HasChangingRooms = v.HasChangingRooms,
            HasLighting = v.HasLighting,
            HasShowers = v.HasShowers,
            IsActive = v.IsActive,
            OwnerName = v.Owner?.FullName ?? "Unknown",
            Photos = v.Photos //TODO: Think about how to handle photos
        }).ToList();

        return data;
    }

    public async Task<Result<List<GetVenueMinimalDto>>> GetVenuesMinimalAsync()
    {
        var venues = await _venueRepository.GetVenuesMinimalAsync();
        var data = venues.Select(v => new GetVenueMinimalDto
        {
            Id = v.Id,
            Name = v.Name,
            Address = v.Address,
            Description = v.Description,
            Latitude = v.Latitude,
            Longitude = v.Longitude,
            PricePerHour = v.PricePerHour,
            MinPlayers = v.MinPlayers,
            MaxPlayers = v.MaxPlayers,
            ContactPhone = v.ContactPhone,
            PrimarySportType = v.PrimarySportType,
            OwnerName = v.Owner?.FullName ?? "Unknown"
        }).ToList();

        return data;
    }

    public async Task<Result<GetVenueMinimalDto?>> GetVenueMinimalByIdAsync(Guid id)
    {
        var venue = await _venueRepository.GetVenueByIdAsync(id);
        if (venue is null)
        {
            return Result.Failure<GetVenueMinimalDto?>("Venue not found");
        }
        var data = new GetVenueMinimalDto
        {
            Id = venue.Id,
            Name = venue.Name,
            Address = venue.Address,
            Description = venue.Description,
            Latitude = venue.Latitude,
            Longitude = venue.Longitude,
            PricePerHour = venue.PricePerHour,
            MinPlayers = venue.MinPlayers,
            MaxPlayers = venue.MaxPlayers,
            ContactPhone = venue.ContactPhone,
            PrimarySportType = venue.PrimarySportType,
            OwnerName = venue.Owner?.FullName ?? "Unknown"
        };

        return data;
    }

    public async Task<Result<GetVenueDto?>> GetVenueByIdAsync(Guid id)
    {
        var venue = await _venueRepository.GetVenueByIdAsync(id);
        if (venue is null)
        {
            return Result.Failure<GetVenueDto?>("Venue not found");
        }

        var data = new GetVenueDto
        {
            Id = venue.Id,
            Name = venue.Name,
            Address = venue.Address,
            Description = venue.Description,
            Latitude = venue.Latitude,
            Longitude = venue.Longitude,
            PricePerHour = venue.PricePerHour,
            MinPlayers = venue.MinPlayers,
            MaxPlayers = venue.MaxPlayers,
            ContactPhone = venue.ContactPhone,
            PrimarySportType = venue.PrimarySportType,
            HasParking = venue.HasParking,
            HasChangingRooms = venue.HasChangingRooms,
            HasLighting = venue.HasLighting,
            HasShowers = venue.HasShowers,
            IsActive = venue.IsActive,
            OwnerName = venue.Owner?.FullName ?? "Unknown",
            Photos = venue.Photos //TODO: Think about how to handle photos
        };

        return data;
    }

    public async Task<Result<GetVenueMinimalDto>> UpdateVenueAsync(UpdateVenueDto dto)
    {
        var venue = await _venueRepository.GetVenueByIdAsync(dto.Id);
        if (venue is null)
        {
            return Result.Failure<GetVenueMinimalDto>("Venue not found");
        }

        if (string.IsNullOrEmpty(dto.Name) == false)
        {
            venue.Name = dto.Name;
        }

        if (string.IsNullOrEmpty(dto.Address) == false)
        {
            venue.Address = dto.Address;
        }

        if (string.IsNullOrEmpty(dto.Description) == false)
        {
            venue.Description = dto.Description;
        }

        if (dto.Latitude.HasValue)
        {
            venue.Latitude = dto.Latitude.Value;
        }
        
        if (dto.Longitude.HasValue)
        {
            venue.Longitude = dto.Longitude.Value;
        }
        if (dto.PricePerHour.HasValue)
        {
            venue.PricePerHour = dto.PricePerHour.Value;
        }
        if (dto.MinPlayers.HasValue)
        {
            venue.MinPlayers = dto.MinPlayers.Value;
        }
        if (dto.MaxPlayers.HasValue)
        {
            venue.MaxPlayers = dto.MaxPlayers.Value;
        }
        if (string.IsNullOrEmpty(dto.ContactPhone) == false)
        {
            venue.ContactPhone = dto.ContactPhone;
        }
        if (dto.PrimarySportType.HasValue)
        {
            venue.PrimarySportType = dto.PrimarySportType.Value;
        }
        if (dto.HasParking.HasValue)
        {
            venue.HasParking = dto.HasParking.Value;
        }
        if (dto.HasChangingRooms.HasValue)
        {
            venue.HasChangingRooms = dto.HasChangingRooms.Value;
        }
        if (dto.HasLighting.HasValue)
        {
            venue.HasLighting = dto.HasLighting.Value;
        }
        if (dto.HasShowers.HasValue)
        {
            venue.HasShowers = dto.HasShowers.Value;
        }
        

        var updated = await _venueRepository.UpdateVenueAsync(venue);
        if (updated is null)
        {
            return Result.Failure<GetVenueMinimalDto>("Error while updating the venue");
        }

        var returning = new GetVenueMinimalDto
        {
            Id = updated.Id,
            Name = updated.Name,
            Address = updated.Address,
            Description = updated.Description,
            Latitude = updated.Latitude,
            Longitude = updated.Longitude,
            PricePerHour = updated.PricePerHour,
            MinPlayers = updated.MinPlayers,
            MaxPlayers = updated.MaxPlayers,
            ContactPhone = updated.ContactPhone,
            PrimarySportType = SportType.Football,
            OwnerName = updated.Owner?.FullName ?? "Unknown"
        };

        return returning;
    }

    public async Task<Result<bool>> DeactivateVenueAsync(Guid id)
    {
        var venue = await _venueRepository.GetVenueByIdAsync(id);
        if (venue is null)
        {
            return Result.Failure<bool>("Venue to delete not found");
        }

        var isDeactivated = await _venueRepository.DeactivateVenueAsync(id);
        return isDeactivated;
    }
}