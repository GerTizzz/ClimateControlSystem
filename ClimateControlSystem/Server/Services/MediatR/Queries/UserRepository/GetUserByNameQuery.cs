using ClimateControlSystem.Server.Resources.Repository.TablesEntities;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Queries.UserRepository;

public sealed class GetUserByNameQuery : IRequest<UserEntity?>
{
    public string Name { get; }

    public GetUserByNameQuery(string name)
    {
        Name = name;
    }
}