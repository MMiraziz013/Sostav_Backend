using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sostav.Domain.Entities;

namespace Sostav.Infrastructure.Persistence.Configurations;

public class VenuePhotoConfiguration : IEntityTypeConfiguration<VenuePhoto>
{
    public void Configure(EntityTypeBuilder<VenuePhoto> builder)
    {
        builder.ToTable("venue_photos");
        
        builder.HasKey(vp => vp.Id);
        
        builder.Property(vp => vp.Id)
            .HasColumnName("id");
        
        builder.Property(vp => vp.Url)
            .HasColumnName("url")
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(vp => vp.IsPrimary)
            .HasColumnName("is_primary")
            .HasDefaultValue(false);
        
        builder.Property(vp => vp.DisplayOrder)
            .HasColumnName("display_order")
            .HasDefaultValue(0);
        
        builder.Property(vp => vp.VenueId)
            .HasColumnName("venue_id");
        
        builder.Property(vp => vp.CreatedAt)
            .HasColumnName("created_at");
        
        builder.Property(vp => vp.UpdatedAt)
            .HasColumnName("updated_at");
        
        // Index for faster queries
        builder.HasIndex(vp => new { vp.VenueId, vp.IsPrimary });
    }
}
