using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sostav.Application.Features.Auth;

namespace Sostav.Application;

public static class ApplicationInjection
{

    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddTransient<IAuthService, AuthService>();
        //TODO: Add application services
        
        return services;
    }
}