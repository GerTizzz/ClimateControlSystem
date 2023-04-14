using Application.Helpers;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.MediatR.UsersRepository;

public sealed class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUsersRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateUserHandler(IUsersRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        TokenHelper.CreatePasswordHash(request.UserDto.Password, out var passwordHash, out var passwordSalt);

        var user = _mapper.Map<User>(request.UserDto);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        var result = await _userRepository.UpdateUser(user);

        return result;
    }
}