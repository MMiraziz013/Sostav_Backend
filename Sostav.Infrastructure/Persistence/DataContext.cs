using Microsoft.EntityFrameworkCore;
using Sostav.Application.Common.Interfaces;
using Sostav.Domain.Common;
using Sostav.Domain.Entities;

namespace Sostav.Infrastructure.Persistence;

public class DataContext : DbContext, IDataContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users => Set<User>();
    public DbSet<Venue> Venues => Set<Venue>();
    public DbSet<VenuePhoto> VenuePhotos => Set<VenuePhoto>();
    public DbSet<Game> Games => Set<Game>();
    public DbSet<GameParticipant> GameParticipants => Set<GameParticipant>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all configurations from THIS assembly (Infrastructure)
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Auto-set CreatedAt and UpdatedAt
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Id = entry.Entity.Id == Guid.Empty ? Guid.NewGuid() : entry.Entity.Id;
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }
        
        return await base.SaveChangesAsync(cancellationToken);
    }
    
    public async Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        await Database.MigrateAsync(cancellationToken);
    }
}
