using MediatR;

namespace Application.MediatR.UsersRepository;

public sealed class GetUserByNameQuery : IRequest<User?>
{
    public string UserName { get; }

    public GetUserByNameQuery(string userName)
    {
        UserName = userName;
    }
}