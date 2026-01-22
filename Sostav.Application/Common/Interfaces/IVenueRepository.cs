using Sostav.Domain.Entities;

namespace Sostav.Application.Common.Interfaces;

public interface IVenueRepository
{
    //TODO: Understand where to implement repository methods.
    Task<Venue?> AddVenueAsync(Venue venue);

    Task<List<Venue>> GetVenuesFullInfoAsync();

    Task<List<Venue>> GetVenuesMinimalAsync();

    Task<Venue?> GetVenueByIdAsync(Guid id);

    Task<Venue?> UpdateVenueAsync(Venue venue);

    Task<bool> DeactivateVenueAsync(Guid id);
}