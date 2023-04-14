using Domain.Repositories;
using MediatR;

namespace Application.MediatR.UsersRepository;

public sealed class GetUserByNameHandler : IRequestHandler<GetUserByNameQuery, User?>
{
    private readonly IUsersRepository _userRepository;

    public GetUserByNameHandler(IUsersRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByName(request.UserName);

        return user;
    }
}