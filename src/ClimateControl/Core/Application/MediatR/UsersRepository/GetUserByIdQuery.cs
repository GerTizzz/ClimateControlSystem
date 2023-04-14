using MediatR;
using Shared.Dtos;

namespace Application.MediatR.UsersRepository;

public sealed class GetUserByIdQuery : IRequest<UserDto?>
{
    public Guid Id { get; }

    public GetUserByIdQuery(Guid id)
    {
        Id = id;
    }
}