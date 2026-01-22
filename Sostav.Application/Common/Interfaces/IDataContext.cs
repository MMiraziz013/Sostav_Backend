using Microsoft.EntityFrameworkCore;
using Sostav.Domain.Entities;

namespace Sostav.Application.Common.Interfaces;

public interface IDataContext
{
    DbSet<User> Users { get; }
    DbSet<Venue> Venues { get; }
    DbSet<VenuePhoto> VenuePhotos { get; }
    DbSet<Game> Games { get; }
    DbSet<GameParticipant> GameParticipants { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task MigrateAsync(CancellationToken cancellationToken = default);
}
