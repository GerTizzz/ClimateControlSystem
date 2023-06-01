using MediatR;
using Shared.Dtos;

namespace Application.MediatR.ConfigsSingleton;

public sealed class GetConfigDtoQuery : IRequest<ConfigsDto>
{

}