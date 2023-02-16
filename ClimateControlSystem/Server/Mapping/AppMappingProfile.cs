using AutoMapper;
using ClimateControlSystem.Server.Protos;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Resources.Repository.TablesEntities;
using ClimateControlSystem.Server.Services.PredictionEngine.PredictionEngineResources;
using ClimateControlSystem.Shared.Common;
using ClimateControlSystem.Shared.SendToClient;
using System.Threading;

namespace ClimateControlSystem.Server.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            #region gRPC communication

            CreateMap<ClimateMonitoringRequest, FeaturesData>();

            #endregion

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

            #region Repository

            CreateMap<Monitoring, MonitoringsEntity>();

            CreateMap<MonitoringsEntity, Monitoring>();


            CreateMap<FeaturesData, FeaturesDataEntity>();

            CreateMap<FeaturesDataEntity, FeaturesData>();


            CreateMap<AccuracysEntity, Accuracy>();

            CreateMap<Accuracy, AccuracysEntity>();


            CreateMap<ActualDataEntity, ActualData>();

            CreateMap<ActualData, ActualDataEntity>();


            CreateMap<PredictionsEntity, Prediction>();

            CreateMap<Prediction, PredictionsEntity>();


            CreateMap<Config, ConfigsEntity>();

            CreateMap<ConfigsEntity, Config>();


            CreateMap<MicroclimatesEventsEntity, MicroclimatesEvents>();

            CreateMap<MicroclimatesEvents, MicroclimatesEventsEntity>();


            CreateMap<ActualData, FeaturesData>();

            CreateMap<FeaturesData, ActualData>();


            CreateMap<UserEntity, UserModelWithCredentials>()
                .ForMember(dto => dto.Name, auth => auth
                    .MapFrom(authSrc => authSrc.Name))
                .ForMember(dto => dto.Role, auth => auth
                    .MapFrom(authSrc => authSrc.Role))
                .ForMember(dto => dto.Id, auth => auth
                    .MapFrom(authSrc => authSrc.Id));

            CreateMap<UserModelWithCredentials, UserEntity>()
                .ForMember(auth => auth.Name, dto => dto
                    .MapFrom(dtoSrc => dtoSrc.Name))
                .ForMember(auth => auth.Role, dto => dto
                    .MapFrom(dtoSrc => dtoSrc.Role));

            #endregion

            #region ClientCommunication

            CreateMap<ConfigResponse, Config>();

            CreateMap<Config, ConfigResponse>();


            CreateMap<Prediction, PredictionResponse>();


            CreateMap<ActualData, ActualDataResponse>();


            CreateMap<Monitoring, BaseMonitoringResponse>();

            CreateMap<Monitoring, MonitoringWithEventsResponse>();

            CreateMap<Monitoring, MonitoringWithAccuraciesResponse>();


            CreateMap<MicroclimatesEvents, MicroclimateEventResponse>();

            #endregion
        }
    }
}
