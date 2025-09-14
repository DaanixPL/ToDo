using Microsoft.Extensions.DependencyInjection;
using App.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using App.Infrastructure.Persistence.Repositories;
using App.Application.Interfaces.Authentication;



namespace App.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenGeneratorRepository, JwtTokenGenerator>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ITodoItemRepository, TodoItemRepository>();

            services.AddJwtServices(configuration);
            services.AddSwaggerServices();
            services.AddCorsServices();
            services.AddDatabaseServices(configuration);
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
