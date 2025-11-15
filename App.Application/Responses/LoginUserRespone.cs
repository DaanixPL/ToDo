namespace ToDo.Application.Responses
{
    public record LoginUserRespone(int UserId, string Username, string AccessToken, string RefreshToken);
}
