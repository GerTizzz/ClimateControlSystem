using AutoMapper;
using Domain.Repositories;
using MediatR;
using Shared.Dtos;

namespace Application.MediatR.WarningsRepository;

public sealed class GetWarningsHandler : IRequestHandler<GetWarningsQuery, List<WarningDto>>
{
    private readonly IMapper _mapper;
    private readonly IWarningsRepository _warningsRepository;

    public GetWarningsHandler(IMapper mapper, IWarningsRepository warningsRepository)
    {
        _mapper = mapper;
        _warningsRepository = warningsRepository;
    }

    public async Task<List<WarningDto>> Handle(GetWarningsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var warnings = await _warningsRepository.GetWarningsAsync(request.RequestLimits);

            var warningsDtos = warnings.Select(entity => _mapper.Map<WarningDto>(entity.Warning)).ToList();

            return warningsDtos;
        }
        catch (Exception)
        {
            return new List<WarningDto>();
        }
    }
}