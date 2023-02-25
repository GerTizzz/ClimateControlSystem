using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Resources.Repository.TablesEntities;
using ClimateControlSystem.Server.Services.MediatR.Queries.UserRepository;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers.UserRepository;

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