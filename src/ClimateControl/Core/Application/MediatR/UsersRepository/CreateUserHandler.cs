using Application.Helpers;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.MediatR.UsersRepository;

public sealed class CreateUserHandler : IRequestHandler<CreateUserCommand, bool>
{
    private readonly IUsersRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserHandler(IUsersRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        TokenHelper.CreatePasswordHash(request.UserDto.Password, out var passwordHash, out var passwordSalt);

        var userEntity = _mapper.Map<User>(request.UserDto);

        userEntity.PasswordHash = passwordHash;
        userEntity.PasswordSalt = passwordSalt;

        var result = await _userRepository.SaveUser(userEntity);

        return result;
    }
}