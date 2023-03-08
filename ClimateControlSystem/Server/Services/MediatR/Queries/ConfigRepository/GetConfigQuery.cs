using MediatR;

namespace ClimateControl.Server.Services.MediatR.Queries.ConfigRepository;

public sealed class GetConfigQuery : IRequest<Config>
{

}