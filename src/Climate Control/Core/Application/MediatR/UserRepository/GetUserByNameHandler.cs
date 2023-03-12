using Domain.Repositories;
using MediatR;

namespace Application.MediatR.UserRepository
{
    public sealed class GetUserByNameHandler : IRequestHandler<GetUserByNameQuery, User?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByNameHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
            var userEntity = await _userRepository.GetUserByName(request.Name);

            return userEntity;
        }
    }
}
