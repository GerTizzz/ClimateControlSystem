using ClimateControlSystem.Shared.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Queries.UserRepository;

public sealed class GetUserByIdQuery : IRequest<UserDto?>
{
    public int Id { get; }

    public GetUserByIdQuery(int id)
    {
        Id = id;
    }
}