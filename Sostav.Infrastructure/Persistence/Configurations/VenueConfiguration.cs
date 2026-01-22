using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sostav.Domain.Entities;

namespace Sostav.Infrastructure.Persistence.Configurations;

public class VenueConfiguration : IEntityTypeConfiguration<Venue>
{
    public void Configure(EntityTypeBuilder<Venue> builder)
    {
        builder.ToTable("venues");
        
        builder.HasKey(v => v.Id);
        
        builder.Property(v => v.Id)
            .HasColumnName("id");
        
        builder.Property(v => v.Name)
            .HasColumnName("name")
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(v => v.Address)
            .HasColumnName("address")
            .HasMaxLength(300)
            .IsRequired();
        
        builder.Property(v => v.Description)
            .HasColumnName("description")
            .HasMaxLength(1000);
        
        builder.Property(v => v.Latitude)
            .HasColumnName("latitude")
            .HasPrecision(10, 8);
        
        builder.Property(v => v.Longitude)
            .HasColumnName("longitude")
            .HasPrecision(11, 8);
        
        builder.Property(v => v.PricePerHour)
            .HasColumnName("price_per_hour")
            .HasPrecision(12, 2);
        
        builder.Property(v => v.MinPlayers)
            .HasColumnName("min_players");
        
        builder.Property(v => v.MaxPlayers)
            .HasColumnName("max_players");
        
        builder.Property(v => v.ContactPhone)
            .HasColumnName("contact_phone")
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(v => v.PrimarySportType)
            .HasColumnName("primary_sport_type")
            .HasConversion<int>();
        
        builder.Property(v => v.HasParking)
            .HasColumnName("has_parking")
            .HasDefaultValue(false);
        
        builder.Property(v => v.HasChangingRooms)
            .HasColumnName("has_changing_rooms")
            .HasDefaultValue(false);
        
        builder.Property(v => v.HasLighting)
            .HasColumnName("has_lighting")
            .HasDefaultValue(false);
        
        builder.Property(v => v.HasShowers)
            .HasColumnName("has_showers")
            .HasDefaultValue(false);
        
        builder.Property(v => v.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true);
        
        builder.Property(v => v.OwnerId)
            .HasColumnName("owner_id");
        
        builder.Property(v => v.CreatedAt)
            .HasColumnName("created_at");
        
        builder.Property(v => v.UpdatedAt)
            .HasColumnName("updated_at");
        
        // Relationships
        builder.HasOne(v => v.Owner)
            .WithMany(u => u.OwnedVenues)
            .HasForeignKey(v => v.OwnerId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasMany(v => v.Photos)
            .WithOne(p => p.Venue)
            .HasForeignKey(p => p.VenueId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(v => v.Games)
            .WithOne(g => g.Venue)
            .HasForeignKey(g => g.VenueId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
