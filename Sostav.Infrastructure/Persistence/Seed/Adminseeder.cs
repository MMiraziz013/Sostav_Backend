using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sostav.Application.Common.Interfaces;
using Sostav.Domain.Entities;
using Sostav.Domain.Enums;

namespace Sostav.Infrastructure.Persistence.Seed;

public class AdminSeeder
{
    private readonly IDataContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<AdminSeeder> _logger;

    public AdminSeeder(
        IDataContext context, 
        IPasswordHasher passwordHasher,
        ILogger<AdminSeeder> logger)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        // Check if admin already exists
        var adminExists = await _context.Users
            .AnyAsync(u => u.Role == UserRole.Admin);

        if (adminExists)
        {
            _logger.LogInformation("Admin user already exists, skipping seed.");
            return;
        }

        // Create admin user
        var admin = new User
        {
            FullName = "Admin",
            Phone = "+998907884422",  // Change this to your phone
            PasswordHash = _passwordHasher.Hash("myPassword1$"),  // Change this password!
            Role = UserRole.Admin,
            City = "Tashkent",
            IsActive = true
        };

        _context.Users.Add(admin);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Admin user created successfully.");
    }
}