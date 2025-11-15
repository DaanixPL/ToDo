using Microsoft.Extensions.DependencyInjection;
using ToDo.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using ToDo.Application.Interfaces.Authentication;
using ToDo.Infrastructure.Persistence.Repositories;

namespace ToDo.Infrastructure.DependencyInjection
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
