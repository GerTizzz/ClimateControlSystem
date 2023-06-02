using Application.gRCP.Protos;
using Application.Helpers;
using Application.Mapping.Converters;
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
            .ConstructUsing(request => new Feature(Guid.NewGuid(), request.TemperatureOutside, request.TemperatureInside, request.CoolingPower));

        CreateMap<Feature, TensorRequest>()
            .ForMember(tensor => tensor.serving_default_lstm_input, opt => opt
                .MapFrom(property => new[]
                {
                    property.TemperatureOutside,
                    property.TemperatureInside,
                    property.CoolingPower
                }));

        CreateMap<TensorResult, IEnumerable<PredictedValue>>()
            .ConvertUsing<TensorResultToPredictionConverter>();

        CreateMap<Forecast, ForecastDto>();
        
        CreateMap<PredictedValue, PredictionDto>();

        CreateMap<Warning, WarningDto>();

        CreateMap<Feature, FeaturesDto>();

        CreateMap<Config, ConfigsDto>();

        CreateMap<User, UserDto>()
            .ForMember(userDto => userDto.Role, user => user
                .MapFrom(u => u.Role.ToString()));

        CreateMap<PredictedValue, WarningDto>()
            .ForMember(dto => dto.Message, monEntity => monEntity
                .MapFrom(entity => entity.Warning.Message));

        CreateMap<ConfigsDto, Config>()
            .ConstructUsing(dto => new Config(Guid.NewGuid(),
            dto.UpperTemperatureWarningLimit,
            dto.LowerTemperatureWarningLimit));

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