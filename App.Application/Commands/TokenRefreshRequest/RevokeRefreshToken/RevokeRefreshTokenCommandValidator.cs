using FluentValidation;
using ToDo.Application.Commands.TokenRefreshRequest.TokenRefresh;
using ToDo.Application.Validators.Extensions;

namespace ToDo.Application.Commands.TokenRefreshRequest.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommandValidator : AbstractValidator<RevokeRefreshTokenCommand>
    {
        public RevokeRefreshTokenCommandValidator() 
        {
            RuleFor(x => x.RefreshToken)
                .RequiredField(nameof(TokenRefreshRequestCommand.AccessToken))
                .NoWhiteSpaces(nameof(TokenRefreshRequestCommand.AccessToken));
        }
    }
}
