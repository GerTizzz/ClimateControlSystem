using Application.Helpers;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.MediatR.UserRepository
{
    public sealed class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            TokenHelper.CreatePasswordHash(request.UserDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var dto = request.UserDto;

            var user = new User(new Guid(dto.Id), dto.Name, dto.Role, passwordHash, passwordSalt);

            var result = await _userRepository.UpdateUser(user);

            return result;
        }
    }
}
