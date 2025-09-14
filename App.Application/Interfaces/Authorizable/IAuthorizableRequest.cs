namespace App.Application.Interfaces.Authorizable
{
    public interface IAuthorizableRequest
    {
        int? ResourceOwnerId { get; }
        bool AllowAdminOverride { get; }
    }
}
