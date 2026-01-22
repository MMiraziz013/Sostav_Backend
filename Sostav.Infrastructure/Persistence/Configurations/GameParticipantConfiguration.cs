using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sostav.Domain.Entities;

namespace Sostav.Infrastructure.Persistence.Configurations;

public class GameParticipantConfiguration : IEntityTypeConfiguration<GameParticipant>
{
    public void Configure(EntityTypeBuilder<GameParticipant> builder)
    {
        builder.ToTable("game_participants");
        
        builder.HasKey(gp => gp.Id);
        
        builder.Property(gp => gp.Id)
            .HasColumnName("id");
        
        builder.Property(gp => gp.JoinedAt)
            .HasColumnName("joined_at")
            .IsRequired();
        
        builder.Property(gp => gp.Status)
            .HasColumnName("status")
            .HasConversion<int>()
            .HasDefaultValue(Domain.Enums.ParticipantStatus.Confirmed);
        
        builder.Property(gp => gp.GameId)
            .HasColumnName("game_id");
        
        builder.Property(gp => gp.UserId)
            .HasColumnName("user_id");
        
        builder.Property(gp => gp.CreatedAt)
            .HasColumnName("created_at");
        
        builder.Property(gp => gp.UpdatedAt)
            .HasColumnName("updated_at");
        
        // Relationships
        builder.HasOne(gp => gp.User)
            .WithMany(u => u.GameParticipations)
            .HasForeignKey(gp => gp.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Unique constraint: User can only join a game once
        builder.HasIndex(gp => new { gp.GameId, gp.UserId })
            .IsUnique();
        
        // Index for user's games
        builder.HasIndex(gp => gp.UserId);
    }
}
