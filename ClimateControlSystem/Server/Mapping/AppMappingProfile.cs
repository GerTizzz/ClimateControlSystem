using AutoMapper;
using ClimateControlSystem.Server.Protos;
using ClimateControlSystem.Server.Resources.Common;
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

            CreateMap<Config, ConfigsDTO>();


            CreateMap<Prediction, PredictionsDTO>();

            CreateMap<ActualData, ActualDataDTO>();


            CreateMap<Monitoring, BaseMonitoringDTO>();

            CreateMap<Monitoring, MonitoringWithEventsDTO>();

            CreateMap<Monitoring, MonitoringWithAccuracyDTO>();

            #endregion

            #region DTO-To-Domain

            CreateMap<ConfigsDTO, Config>();

            #endregion


            #region Repository-To-DTO

            CreateMap<MonitoringsEntity, MonitoringWithEventsDTO>();

            CreateMap<MonitoringsEntity, MonitoringWithAccuracyDTO>();

            CreateMap<MonitoringsEntity, BaseMonitoringDTO>();


            CreateMap<MicroclimatesEvents, MicroclimatesEventsDTO>();

            CreateMap<PredictionsEntity, PredictionsDTO>();

            CreateMap<ActualDataEntity, ActualDataDTO>();

            CreateMap<MicroclimatesEventsEntity, MicroclimatesEventsDTO>();

            CreateMap<AccuracysEntity, AccuracyDTO>();


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
