using MediatR;

namespace Application.MediatR.UserRepository
{
    public sealed class GetUserByNameQuery : IRequest<User?>
    {
        public string Name { get; }

        public GetUserByNameQuery(string name)
        {
            Name = name;
        }
    }
}
