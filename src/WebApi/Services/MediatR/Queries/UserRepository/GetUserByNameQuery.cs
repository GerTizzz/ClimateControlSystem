using MediatR;
using WebApi.Resources.Repository.TablesEntities;

namespace WebApi.Services.MediatR.Queries.UserRepository
{
    public sealed class GetUserByNameQuery : IRequest<UserEntity?>
    {
        public string Name { get; }

        public GetUserByNameQuery(string name)
        {
            Name = name;
        }
    }
}