using Application.gRCP.Protos;
using Application.Helpers;
using Application.Primitives;
using AutoMapper;
using Domain.Enumerations;
using Shared.Dtos;

namespace Application.Mapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<GrpcForecastRequest, Feature>()
            .ConstructUsing(request => new Feature(Guid.NewGuid()));

        CreateMap<Feature, TensorPredictionRequest>()
            .ForMember(tensor => tensor.serving_default_lstm_input, opt => opt
                .MapFrom(property => new[]
                {
                    property.TemperatureInside,
                    property.TemperatureOutside,
                    property.CoolingPower
                }));

        CreateMap<TensorPredictionResult, PredictedValue>()
            .ForMember(result => result.Values, tensor => tensor
                .MapFrom(tensorSrc => tensorSrc.StatefulPartitionedCall[0]))
            .ConstructUsing(result => new PredictedValue(Guid.NewGuid()));

        CreateMap<Forecast, ForecastDto>();
        
        CreateMap<ActualValue, FactDto>();

        CreateMap<PredictedValue, LabelDto>();

        CreateMap<Warning, WarningDto>();

        CreateMap<Feature, FeaturesDto>();

        CreateMap<Config, ConfigsDto>();

        CreateMap<User, UserDto>()
            .ForMember(userDto => userDto.Role, user => user
                .MapFrom(u => u.Role.ToString()));

        /*CreateMap<Forecast, WarningDto>()
            .ForMember(dto => dto.Value, monEntity => monEntity
                .MapFrom(entity => entity.Warning.Value));*/

        CreateMap<ConfigsDto, Config>();

        CreateMap<UserDto, User?>()
            .ConstructUsing(x => CreateUserEntityFromDto(x));
    }

    private static User? CreateUserEntityFromDto(UserDto userDto)
    {
        var isParsed = Enum.TryParse(userDto.Role, false, out UserType userType);

        if (isParsed is false)
        {
            return null;
        }

        TokenHelper.CreatePasswordHash(userDto.Password, out var passwordHash, out var passwordSalt);

        return new User(userDto.Id, userDto.Name, userType, passwordHash, passwordSalt);
    }
}