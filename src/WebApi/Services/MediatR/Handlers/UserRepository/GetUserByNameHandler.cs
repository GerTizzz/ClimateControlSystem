using MediatR;
using WebApi.Infrastructure.Repositories;
using WebApi.Resources.Repository.TablesEntities;
using WebApi.Services.MediatR.Queries.UserRepository;

namespace WebApi.Services.MediatR.Handlers.UserRepository
{
    public sealed class GetUserByNameHandler : IRequestHandler<GetUserByNameQuery, UserEntity?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByNameHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserEntity?> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
            var userEntity = await _userRepository.GetUserByName(request.Name);

            return userEntity;
        }
    }
}