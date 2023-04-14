using AutoMapper;
using Domain.Repositories;
using MediatR;
using Shared.Dtos;

namespace Application.MediatR.UsersRepository;

public sealed class GetUsersHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly IUsersRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersHandler(IUsersRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var usersEntities = await _userRepository.GetUsers();

        var usersDtos = usersEntities.Select(user => _mapper.Map<UserDto>(user)).ToList();

        return usersDtos;
    }
}