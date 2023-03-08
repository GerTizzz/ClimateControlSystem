using ClimateControl.Shared.Dtos;
using MediatR;

namespace ClimateControl.Server.Services.MediatR.Queries.UserRepository;

public sealed class GetUsersQuery : IRequest<List<UserDto>>
{

}