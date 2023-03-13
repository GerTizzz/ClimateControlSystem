using Application.gRCP.Protos;
using Application.Helpers;
using Application.Primitives;
using AutoMapper;
using Domain.Enumerations;
using Shared.Dtos;

namespace Application.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<GrpcForecastRequest, Feature>();

            CreateMap<Fact, Feature>();

            CreateMap<Feature, Fact>();

            CreateMap<Feature, TensorPredictionRequest>()
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
                    }));

            CreateMap<TensorPredictionResult, Label>()
                .ForMember(result => result.Temperature, tensor => tensor
                    .MapFrom(tensorSrc => tensorSrc.StatefulPartitionedCall[0]))
                .ForMember(result => result.Humidity, tensor => tensor
                    .MapFrom(tensorSrc => tensorSrc.StatefulPartitionedCall[1]));

            CreateMap<Config, ConfigsDto>();

            CreateMap<Label, LabelDto>();

            CreateMap<Fact, FactDto>();

            CreateMap<Feature, FeaturesDto>();

            CreateMap<Forecast, ForecastDto>();

            CreateMap<Forecast, WarningDto>()
                .ForMember(dto => dto.TemperatureValue, monEntity => monEntity
                    .MapFrom(entity => entity.Warning.Temperature))
                .ForMember(dto => dto.HumidityValue, monEntity => monEntity
                    .MapFrom(entity => entity.Warning.Humidity));

            CreateMap<Warning, WarningDto>();

            CreateMap<Label, LabelDto>();

            CreateMap<User, UserDto>()
                .ForMember(userDto => userDto.Role, user => user
                    .MapFrom(u => u.Role.ToString()));

            CreateMap<ConfigsDto, Config>();

            CreateMap<UserDto, User?>()
                .ConstructUsing(x => CreateUserEntityFromDto(x));
        }

        private User? CreateUserEntityFromDto(UserDto userDto)
        {
            bool isParsed = Enum.TryParse(userDto.Role, false, out UserType userType);

            if (isParsed is false)
            {
                return null;
            }

            TokenHelper.CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            return new User(userDto.Id, userDto.Name, userType, passwordHash, passwordSalt);
        }
    }
}
