using ClimateControl.Shared.Dtos;
using MediatR;

namespace ClimateControl.Server.Services.MediatR.Queries.ConfigManager;

public sealed class GetConfigDtoQuery : IRequest<ConfigsDto>
{

}