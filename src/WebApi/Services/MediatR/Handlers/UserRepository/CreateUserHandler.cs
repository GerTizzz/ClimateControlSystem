using AutoMapper;
using MediatR;
using WebApi.Helpers;
using WebApi.Infrastructure.Repositories;
using WebApi.Resources.Repository.TablesEntities;
using WebApi.Services.MediatR.Commands.UserRepository;

namespace WebApi.Services.MediatR.Handlers.UserRepository
{
    public sealed class CreateUserHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            TokenHelper.CreatePasswordHash(request.UserDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var userEntity = _mapper.Map<UserEntity>(request.UserDto);

            userEntity.PasswordHash = passwordHash;
            userEntity.PasswordSalt = passwordSalt;

            var result = await _userRepository.SaveUser(userEntity);

            return result;
        }
    }
}