using App.Application.Commands.TokenRefreshRequest.TokenRefresh;
using App.Application.Validators.Extensions;
using FluentValidation;

namespace App.Application.Commands.TokenRefreshRequest.RevokeRefreshToken
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
