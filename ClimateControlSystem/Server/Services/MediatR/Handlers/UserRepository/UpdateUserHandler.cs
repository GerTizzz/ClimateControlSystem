using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Resources.Authentication;
using ClimateControlSystem.Server.Resources.Repository.TablesEntities;
using ClimateControlSystem.Server.Services.MediatR.Commands.UserRepository;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers.UserRepository;

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

        var userEntity = _mapper.Map<UserEntity>(request.UserDto);

        userEntity.PasswordHash = passwordHash;
        userEntity.PasswordSalt = passwordSalt;
        
        var result = await _userRepository.UpdateUser(userEntity, request.Id);
        
        return result;
    }
}