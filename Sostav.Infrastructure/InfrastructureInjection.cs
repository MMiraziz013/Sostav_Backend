using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sostav.Application.Common.Interfaces;
using Sostav.Infrastructure.Persistence;
using Sostav.Infrastructure.Services;

namespace Sostav.Infrastructure;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
            // Database - with environment variable support for password
            var baseconnectionString = configuration.GetConnectionString("DefaultConnection");
        
            var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
            if (!string.IsNullOrEmpty(dbPassword))
            {
                baseconnectionString = baseconnectionString?.Replace("Password=", $"Password={dbPassword}");
            }
        
            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(baseconnectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(DataContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(3);
                }));
        
            services.AddScoped<IDataContext>(provider => 
                provider.GetRequiredService<DataContext>());
        
            // Services
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        
            return services;
    }
}