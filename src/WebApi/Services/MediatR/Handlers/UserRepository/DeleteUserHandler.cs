using MediatR;
using WebApi.Infrastructure.Repositories;
using WebApi.Services.MediatR.Commands.UserRepository;

namespace WebApi.Services.MediatR.Handlers.UserRepository
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