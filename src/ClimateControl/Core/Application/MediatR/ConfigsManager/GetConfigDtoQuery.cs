using MediatR;
using Shared.Dtos;

namespace Application.MediatR.ConfigsManager;

public sealed class GetConfigDtoQuery : IRequest<ConfigsDto>
{

}