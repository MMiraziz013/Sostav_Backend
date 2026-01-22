using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sostav.Domain.Entities;

namespace Sostav.Infrastructure.Persistence.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("games");
        
        builder.HasKey(g => g.Id);
        
        builder.Property(g => g.Id)
            .HasColumnName("id");
        
        builder.Property(g => g.SportType)
            .HasColumnName("sport_type")
            .HasConversion<int>();
        
        builder.Property(g => g.ScheduledAt)
            .HasColumnName("scheduled_at")
            .IsRequired();
        
        builder.Property(g => g.DurationMinutes)
            .HasColumnName("duration_minutes")
            .HasDefaultValue(60);
        
        builder.Property(g => g.MaxPlayers)
            .HasColumnName("max_players")
            .IsRequired();
        
        builder.Property(g => g.PricePerPlayer)
            .HasColumnName("price_per_player")
            .HasPrecision(12, 2);
        
        builder.Property(g => g.Description)
            .HasColumnName("description")
            .HasMaxLength(500);
        
        builder.Property(g => g.Status)
            .HasColumnName("status")
            .HasConversion<int>()
            .HasDefaultValue(Domain.Enums.GameStatus.Open);
        
        builder.Property(g => g.IsPublic)
            .HasColumnName("is_public")
            .HasDefaultValue(true);
        
        builder.Property(g => g.VenueId)
            .HasColumnName("venue_id");
        
        builder.Property(g => g.OrganizerId)
            .HasColumnName("organizer_id");
        
        builder.Property(g => g.CreatedAt)
            .HasColumnName("created_at");
        
        builder.Property(g => g.UpdatedAt)
            .HasColumnName("updated_at");
        
        // Ignore computed properties
        builder.Ignore(g => g.CurrentPlayerCount);
        builder.Ignore(g => g.SpotsAvailable);
        builder.Ignore(g => g.IsFull);
        
        // Relationships
        builder.HasOne(g => g.Organizer)
            .WithMany(u => u.OrganizedGames)
            .HasForeignKey(g => g.OrganizerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(g => g.Participants)
            .WithOne(gp => gp.Game)
            .HasForeignKey(gp => gp.GameId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Indexes for common queries
        builder.HasIndex(g => g.ScheduledAt);
        builder.HasIndex(g => g.Status);
        builder.HasIndex(g => new { g.VenueId, g.ScheduledAt });
        builder.HasIndex(g => new { g.SportType, g.Status, g.ScheduledAt });
    }
}
