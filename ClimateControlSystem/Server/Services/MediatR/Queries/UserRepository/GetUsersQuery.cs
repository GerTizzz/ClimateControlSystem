using ClimateControlSystem.Shared.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Queries.UserRepository;

public sealed class GetUsersQuery : IRequest<List<UserDto>>
{
    
}