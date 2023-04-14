using MediatR;

namespace Application.MediatR.WarningsRepository;

public sealed class GetWarningsCountQuery : IRequest<long>
{
}