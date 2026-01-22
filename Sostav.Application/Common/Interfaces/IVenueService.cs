using Sostav.Application.Common.Models;
using Sostav.Application.Features.Venue.Dtos;

namespace Sostav.Application.Common.Interfaces;

public interface IVenueService
{
    //TODO: Implement venue service features

    Task<Result<GetVenueMinimalDto>> AddVenueAsync(AddVenueDto dto);

    Task<Result<List<GetVenueDto>>> GetVenuesFullInfoAsync();

    Task<Result<List<GetVenueMinimalDto>>> GetVenuesMinimalAsync();

    Task<Result<GetVenueMinimalDto?>> GetVenueMinimalByIdAsync(Guid id);

    Task<Result<GetVenueDto?>> GetVenueByIdAsync(Guid id);

    Task<Result<GetVenueMinimalDto>> UpdateVenueAsync(UpdateVenueDto dto);

    Task<Result<bool>> DeactivateVenueAsync(Guid id);
}