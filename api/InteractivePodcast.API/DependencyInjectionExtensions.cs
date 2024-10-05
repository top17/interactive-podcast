using InteractivePodcast.Data.Interfaces;
using InteractivePodcast.Data.Repositories;
using InteractivePodcast.Services.Implementations;
using InteractivePodcast.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace InteractivePodcast.API
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            return services;
        }
    }
}
