using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Application.Behaviors;
using ToDo.Application.Commands.Users.AddUser;
using ToDo.Application.Mappings;

namespace ToDo.Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(AddUserCommand).Assembly));

            services.AddValidatorsFromAssemblyContaining<AddUserCommandValidator>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(UserMappingProfile).Assembly));

            return services;
        }
    }
}
