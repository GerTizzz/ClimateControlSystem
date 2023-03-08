using ClimateControl.Shared.Dtos;
using MediatR;

namespace ClimateControl.Server.Services.MediatR.Queries.UserRepository;

public sealed class GetUserByIdQuery : IRequest<UserDto?>
{
    public int Id { get; }

    public GetUserByIdQuery(int id)
    {
        Id = id;
    }
}