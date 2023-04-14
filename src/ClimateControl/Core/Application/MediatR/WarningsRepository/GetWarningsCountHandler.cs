using Domain.Repositories;
using MediatR;

namespace Application.MediatR.WarningsRepository;

public sealed class GetWarningsCountHandler : IRequestHandler<GetWarningsCountQuery, long>
{
    private readonly IWarningsRepository _warningsRepository;

    public GetWarningsCountHandler(IWarningsRepository warningsRepository)
    {
        _warningsRepository = warningsRepository;
    }

    public async Task<long> Handle(GetWarningsCountQuery request, CancellationToken cancellationToken)
    {
        return await _warningsRepository.GetWarningsCountAsync();
    }
}