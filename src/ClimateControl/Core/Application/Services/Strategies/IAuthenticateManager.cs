namespace Application.Services.Strategies
{
    public interface IAuthenticateManager
    {
        Task<string?> TryGetToken(Guid userId, string password);
    }
}
