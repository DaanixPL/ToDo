using ToDo.Domain.Entities;

namespace App.Application.Interfaces.Authentication
{
    public interface ITokenGeneratorRepository
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken(User user, Guid tokenId);
    }
}
