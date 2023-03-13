using Domain.Repositories;
using MediatR;

namespace Application.MediatR.UserRepository.Delete
{
    public sealed class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.DeleteUser(request.Id);

            return result;
        }
    }
}
