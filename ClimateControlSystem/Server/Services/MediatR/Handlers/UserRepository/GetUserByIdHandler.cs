using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Queries.UserRepository;
using ClimateControlSystem.Shared.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers.UserRepository;

public sealed class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetUser(request.Id);
        
        var userDto = _mapper.Map<UserDto>(userEntity);
        
        return userDto;
    }
}