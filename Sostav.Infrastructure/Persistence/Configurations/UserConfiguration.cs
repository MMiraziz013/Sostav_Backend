using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sostav.Domain.Entities;

namespace Sostav.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Id)
            .HasColumnName("id");
        
        builder.Property(u => u.FullName)
            .HasColumnName("full_name")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(u => u.Phone)
            .HasColumnName("phone")
            .HasMaxLength(20)
            .IsRequired();
        
        builder.HasIndex(u => u.Phone)
            .IsUnique();
        
        builder.Property(u => u.PasswordHash)
            .HasColumnName("password_hash")
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(u => u.TelegramUsername)
            .HasColumnName("telegram_username")
            .HasMaxLength(50);
        
        builder.Property(u => u.AvatarUrl)
            .HasColumnName("avatar_url")
            .HasMaxLength(500);
        
        builder.Property(u => u.City)
            .HasColumnName("city")
            .HasMaxLength(50)
            .HasDefaultValue("Tashkent");
        
        builder.Property(u => u.Role)
            .HasColumnName("role")
            .HasConversion<int>();
        
        builder.Property(u => u.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true);
        
        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at");
        
        builder.Property(u => u.UpdatedAt)
            .HasColumnName("updated_at");
    }
}
