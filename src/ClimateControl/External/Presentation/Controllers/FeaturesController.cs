using Application.MediatR.ForecastsRepository;
using Application.Primitives;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public sealed class FeaturesController : ControllerBase
{
    private readonly IMediator _mediator;

    public FeaturesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("count")]
    public async Task<ActionResult<long>> GetFeaturesCount()
    {
        var recordsCount = await _mediator.Send(new GetForecastsCountQuery());

        return Ok(recordsCount);
    }

    [HttpGet("range/{start:int:min(0)}/{count:int:min(1)}")]
    public async Task<ActionResult<List<FeaturesDto>>> GetFeatures(int start, int count)
    {
        var records = await _mediator.Send(new GetFeaturesQuery(new DbRangeRequest(start, count)));

        return Ok(records);
    }
}
