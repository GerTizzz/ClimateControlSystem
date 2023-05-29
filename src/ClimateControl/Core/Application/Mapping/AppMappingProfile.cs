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

        /*CreateMap<Feature, TensorPredictionRequest>()
            .ForMember(tensor => tensor.serving_default_input_1, opt => opt
                .MapFrom(property => new[]
                {
                    property.ClusterLoad,
                    property.CpuUsage,
                    property.ClusterTemperature,
                    property.Temperature,
                    property.Humidity,
                    property.AirHumidityOutside,
                    property.AirDryTemperatureOutside,
                    property.AirWetTemperatureOutside,
                    property.WindSpeed,
                    property.WindDirection,
                    property.WindEnthalpy,
                    property.CoolingValue
                }));*/

        CreateMap<TensorPredictionResult, Label>()
            .ForMember(result => result.Temperature, tensor => tensor
                .MapFrom(tensorSrc => tensorSrc.StatefulPartitionedCall[0]))
            .ForMember(result => result.Humidity, tensor => tensor
                .MapFrom(tensorSrc => tensorSrc.StatefulPartitionedCall[1]))
            .ConstructUsing(result => new Label(Guid.NewGuid()));

        CreateMap<Forecast, ForecastDto>();

        CreateMap<Error, ErrorDto>();

        CreateMap<Fact, FactDto>();

        CreateMap<Label, LabelDto>();

        CreateMap<Warning, WarningDto>();

        CreateMap<Feature, FeaturesDto>();

        CreateMap<Config, ConfigsDto>();

        CreateMap<User, UserDto>()
            .ForMember(userDto => userDto.Role, user => user
                .MapFrom(u => u.Role.ToString()));

        CreateMap<Forecast, WarningDto>()
            .ForMember(dto => dto.Temperature, monEntity => monEntity
                .MapFrom(entity => entity.Warning.Temperature))
            .ForMember(dto => dto.Humidity, monEntity => monEntity
                .MapFrom(entity => entity.Warning.Humidity));



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