using App.Application.Validators.Extensions;
using FluentValidation;

namespace App.Application.Commands.TokenRefreshRequest.TokenRefresh
{
    public class TokenRefreshRequestCommandValidator : AbstractValidator<TokenRefreshRequestCommand>
    {
        public TokenRefreshRequestCommandValidator() 
        {
            RuleFor(x => x.AccessToken)
                .RequiredField(nameof(TokenRefreshRequestCommand.AccessToken))
                .NoWhiteSpaces(nameof(TokenRefreshRequestCommand.AccessToken));

            RuleFor(x => x.RefreshToken)
                .RequiredField(nameof(TokenRefreshRequestCommand.RefreshToken))
                .NoWhiteSpaces(nameof(TokenRefreshRequestCommand.RefreshToken));
        }
    }
}
