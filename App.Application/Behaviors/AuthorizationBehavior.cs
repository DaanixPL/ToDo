using App.Application.Interfaces.Authorizable;
using App.Application.Validators.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using ToDo.Domain.Entities;

namespace App.Application.Behaviors
{
    public class AuthorizationBehavior<TRequest, TRespons> : IPipelineBehavior<TRequest, TRespons>
        where TRequest : notnull
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthorizationBehavior<TRequest, TRespons>> _logger;

        public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor, ILogger<AuthorizationBehavior<TRequest, TRespons>> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<TRespons> Handle(TRequest request, RequestHandlerDelegate<TRespons> next, CancellationToken cancellationToken)
        {
            if (request is IAuthorizableRequest authRequest)
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user == null || !user.Identity?.IsAuthenticated == true)
                {
                    _logger.LogWarning("Unauthorized access attempt to {RequestName} from IP: {RemoteIp}",
                         typeof(TRequest).Name,
                         _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress);

                    throw new UnauthorizedAccessException();
                }

                var userId = int.Parse(user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
                var isAdmin = user.IsInRole("Admin");

                if(userId != authRequest.ResourceOwnerId)
                {
                    if (!(authRequest.AllowAdminOverride || isAdmin))
                    {
                        _logger.LogWarning("Forbidden access attempt to {RequestName} by User {UserId}",
                            typeof(TRequest).Name,
                            userId);
                        throw new ForbiddenException("User", userId);
                    }
                }
                _logger.LogDebug("User {UserId} successfully authorized for request {RequestName}",
                   userId,
                   typeof(TRequest).Name);
            }
            return await next();
        }
    }
}
