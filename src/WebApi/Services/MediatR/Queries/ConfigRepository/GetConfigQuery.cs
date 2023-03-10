using Domain.Entities;
using MediatR;

namespace WebApi.Services.MediatR.Queries.ConfigRepository
{
    public sealed class GetConfigQuery : IRequest<Config>
    {

    }
}