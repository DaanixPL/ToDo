using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Infrastructure.Persistence.Data.DbContexts;

namespace ToDo.Infrastructure.DependencyInjection
{
    public static class DatabaseServiceRegistration
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration, string)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(8, 0, 29))));

            return services;
        }
    }
}
