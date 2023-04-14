using AutoMapper;
using Domain.Repositories;
using MediatR;
using Shared.Dtos;

namespace Application.MediatR.UsersRepository;

public sealed class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUsersRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IUsersRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.Id);

        var userDto = _mapper.Map<UserDto>(user);
        
        return userDto;
    }
}