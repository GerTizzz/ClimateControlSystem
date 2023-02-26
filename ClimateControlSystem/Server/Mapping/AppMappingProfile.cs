using AutoMapper;
using ClimateControlSystem.Server.Protos;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Resources.Domain;
using ClimateControlSystem.Server.Resources.Repository.TablesEntities;
using ClimateControlSystem.Server.Services.PredictionEngine.PredictionEngineResources;
using ClimateControlSystem.Shared.Common;
using ClimateControlSystem.Shared.Responses;

namespace ClimateControlSystem.Server.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            #region gRPC-To-Domain

            CreateMap<ClimateMonitoringRequest, FeaturesData>();

            #endregion

            #region Domain-To-Domain

            CreateMap<ActualData, FeaturesData>();

            CreateMap<FeaturesData, ActualData>();

            #region PredictEngine

            CreateMap<FeaturesData, TensorPredictionRequest>()
                .ForMember(tensor => tensor.serving_default_input_1, opt => opt
                    .MapFrom(property => new float[]
                    {
                        property.ClusterLoad,
                        property.CpuUsage,
                        property.ClusterTemperature,
                        property.MeasuredTemperature,
                        property.MeasuredHumidity,
                        property.AirHumidityOutside,
                        property.AirDryTemperatureOutside,
                        property.AirWetTemperatureOutside,
                        property.WindSpeed,
                        property.WindDirection,
                        property.WindEnthalpy,
                        property.MeanCoolingValue
                    }));

            CreateMap<TensorPredictionResult, Prediction>()
                .ForMember(result => result.Temperature, tensor => tensor
                    .MapFrom(tensorSrc => tensorSrc.StatefulPartitionedCall[0]))
                .ForMember(result => result.Humidity, tensor => tensor
                    .MapFrom(tensorSrc => tensorSrc.StatefulPartitionedCall[1]));

            #endregion

            #endregion


            #region Repository-To-Domain

            CreateMap<MonitoringsEntity, Monitoring>();

            CreateMap<FeaturesDataEntity, FeaturesData>();

            CreateMap<AccuracysEntity, Accuracy>();

            CreateMap<ActualDataEntity, ActualData>();

            CreateMap<PredictionsEntity, Prediction>();

            CreateMap<ConfigsEntity, Config>();

            CreateMap<MicroclimatesEventsEntity, MicroclimatesEvents>();

            #endregion

            #region Domain-To-Repository

            CreateMap<Monitoring, MonitoringsEntity>();

            CreateMap<Config, ConfigsEntity>();

            CreateMap<FeaturesData, FeaturesDataEntity>();

            CreateMap<Accuracy, AccuracysEntity>();

            CreateMap<ActualData, ActualDataEntity>();

            CreateMap<Prediction, PredictionsEntity>();

            CreateMap<MicroclimatesEvents, MicroclimatesEventsEntity>();

            #endregion


            #region Domain-To-DTO

            CreateMap<Config, ConfigsDto>();


            CreateMap<Prediction, PredictionDto>();

            CreateMap<ActualData, ActualDataDto>();


            CreateMap<Monitoring, BaseMonitoringDto>();

            CreateMap<Monitoring, MonitoringWithEventsDto>();

            CreateMap<Monitoring, MonitoringWithAccuracyDto>();

            #endregion

            #region DTO-To-Domain

            CreateMap<ConfigsDto, Config>();

            #endregion


            #region Repository-To-DTO

            CreateMap<MonitoringsEntity, MonitoringWithEventsDto>();

            CreateMap<MonitoringsEntity, MonitoringWithAccuracyDto>();

            CreateMap<MonitoringsEntity, BaseMonitoringDto>();

            CreateMap<MonitoringsEntity, ForecastingDto>()
                .ForMember(dto => dto.Features, monEntity => monEntity
                    .MapFrom(entity => entity.Prediction.Features));

            CreateMap<MicroclimatesEvents, MicroclimatesEventsDto>();

            CreateMap<PredictionsEntity, PredictionDto>();

            CreateMap<ActualDataEntity, ActualDataDto>();

            CreateMap<MicroclimatesEventsEntity, MicroclimatesEventsDto>();

            CreateMap<AccuracysEntity, AccuracyDto>();

            CreateMap<FeaturesDataEntity, FeaturesDto>();

            CreateMap<UserEntity, UserDto>()
                .ForMember(dto => dto.Name, auth => auth
                    .MapFrom(authSrc => authSrc.Name))
                .ForMember(dto => dto.Role, auth => auth
                    .MapFrom(authSrc => authSrc.Role))
                .ForMember(dto => dto.Id, auth => auth
                    .MapFrom(authSrc => authSrc.Id));

            #endregion

            #region DTO-To-Repository

            CreateMap<UserDto, UserEntity>()
                .ForMember(auth => auth.Name, dto => dto
                    .MapFrom(dtoSrc => dtoSrc.Name))
                .ForMember(auth => auth.Role, dto => dto
                    .MapFrom(dtoSrc => dtoSrc.Role));

            #endregion
        }
    }
}
