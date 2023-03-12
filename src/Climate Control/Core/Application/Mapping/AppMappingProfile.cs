using Application.gRCP.Protos;
using Application.Primitives;
using AutoMapper;
using Shared.Dtos;

namespace Application.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            #region gRPC-To-Domain

            CreateMap<ClimateMonitoringRequest, Feature>();

            #endregion


            #region Domain-To-Domain

            CreateMap<Fact, Feature>();

            CreateMap<Feature, Fact>();

            #region PredictEngine

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

            #endregion

            #endregion


            #region Domain-To-DTO

            CreateMap<Config, ConfigsDto>();


            CreateMap<Label, PredictionDto>();

            CreateMap<Fact, ActualDataDto>();

            CreateMap<Feature, FeaturesDto>();


            CreateMap<Forecast, BaseMonitoringDto>();

            CreateMap<Forecast, MonitoringWithEventsDto>();

            CreateMap<Forecast, MonitoringWithAccuracyDto>();

            #endregion

            #region DTO-To-Domain

            CreateMap<ConfigsDto, Config>();

            #endregion


            #region Repository-To-DTO

            CreateMap<Forecast, MonitoringWithEventsDto>();

            CreateMap<Forecast, MonitoringWithAccuracyDto>();

            CreateMap<Forecast, BaseMonitoringDto>();

            CreateMap<Forecast, MonitoringsEventsDto>()
                .ForMember(dto => dto.TemperatureValue, monEntity => monEntity
                    .MapFrom(entity => entity.Warning.Temperature))
                .ForMember(dto => dto.HumidityValue, monEntity => monEntity
                    .MapFrom(entity => entity.Warning.Humidity));

            CreateMap<Forecast, ForecastingDto>();

            CreateMap<Warning, MicroclimatesEventsDto>();

            CreateMap<Label, PredictionDto>();

            CreateMap<Warning, MicroclimatesEventsDto>();

            CreateMap<User, UserDto>()
                .ForMember(dto => dto.Id, auth => auth
                    .MapFrom(authSrc => authSrc.Id.ToString()));

            #endregion
        }
    }
}
