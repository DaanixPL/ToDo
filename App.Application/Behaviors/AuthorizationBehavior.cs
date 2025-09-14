using App.Application.Interfaces.Authorizable;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace App.Application.Behaviors
{
    public class AuthorizationBehavior<TRequest, TRespons> : IPipelineBehavior<TRequest, TRespons>
        where TRequest : notnull
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TRespons> Handle(TRequest request, RequestHandlerDelegate<TRespons> next, CancellationToken cancellationToken)
        {
            if (request is IAuthorizableRequest authRequest)
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user == null || !user.Identity?.IsAuthenticated == true)
                {
                    throw new UnauthorizedAccessException();
                }

                var userId = int.Parse(user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
                var isAdmin = user.IsInRole("Admin");

                if (!isAdmin || !authRequest.AllowAdminOverride)
                {
                    throw new UnauthorizedAccessException("Forbidden: not owner of resource");
                }
            }

            return await next();
        }
    }
}
