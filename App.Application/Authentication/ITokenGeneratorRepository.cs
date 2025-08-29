using App.Domain.Entities;

namespace App.Application.Authentication
{
    public interface ITokenGeneratorRepository
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken(User user, Guid tokenId);
    }
}
