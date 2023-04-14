using Shared.Dtos;

namespace Application.Services.Strategies;

public interface IAuthenticateManager
{
    Task<string?> TryGetToken(UserDto user, string password);
}