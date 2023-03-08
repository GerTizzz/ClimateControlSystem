using ClimateControl.Server.Resources.Repository.TablesEntities;
using MediatR;

namespace ClimateControl.Server.Services.MediatR.Queries.UserRepository;

public sealed class GetUserByNameQuery : IRequest<UserEntity?>
{
    public string Name { get; }

    public GetUserByNameQuery(string name)
    {
        Name = name;
    }
}