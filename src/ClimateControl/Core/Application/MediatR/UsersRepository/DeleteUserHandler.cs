using Domain.Repositories;
using MediatR;

namespace Application.MediatR.UsersRepository;

public sealed class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUsersRepository _userRepository;

    public DeleteUserHandler(IUsersRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.DeleteUser(request.Id);

        return result;
    }
}