using Microsoft.Extensions.DependencyInjection;

namespace ToDo.Infrastructure.DependencyInjection
{
    public static class CorsServiceRegistration
    {
        public static IServiceCollection AddCorsServices(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrgin",
                    builder =>
                    {
                        builder.WithOrigins("https://todo.vdanix.dev")
                            .AllowAnyMethod()
                            .AllowAnyHeader();

                    });
            });
            return services;
        }
    }
}
