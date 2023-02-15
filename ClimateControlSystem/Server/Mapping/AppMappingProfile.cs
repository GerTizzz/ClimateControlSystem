using AutoMapper;
using ClimateControlSystem.Server.Protos;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Resources.Repository.TablesEntities;
using ClimateControlSystem.Server.Services.PredictionEngine.PredictionEngineResources;
using ClimateControlSystem.Shared.Common;
using ClimateControlSystem.Shared.SendToClient;

namespace ClimateControlSystem.Server.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            #region gRPC communication

            CreateMap<ClimateMonitoringRequest, SensorsData>();

            #endregion

            #region PredictEngine

            CreateMap<SensorsData, TensorPredictionRequest>()
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
                .ForMember(result => result.PredictedTemperature, tensor => tensor
                    .MapFrom(tensorSrc => tensorSrc.StatefulPartitionedCall[0]))
                .ForMember(result => result.PredictedHumidity, tensor => tensor
                    .MapFrom(tensorSrc => tensorSrc.StatefulPartitionedCall[1]));

            #endregion
            
            #region Repository

            CreateMap<SensorsData, SensorsDataEntity>();

            CreateMap<SensorsDataEntity, SensorsData>();


            CreateMap<AccuracysEntity, Accuracy>();

            CreateMap<Accuracy, AccuracysEntity>();


            CreateMap<PredictionsEntity, Prediction>();

            CreateMap<Prediction, PredictionsEntity>();


            CreateMap<Config, ConfigEntity>();

            CreateMap<ConfigEntity, Config>();


            CreateMap<MicroclimatesEventsEntity, MicroclimateEvent>();

            CreateMap<MicroclimateEvent, MicroclimatesEventsEntity>();


            CreateMap<MeasuredData, SensorsData>();

            CreateMap<SensorsData, MeasuredData>();


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


            CreateMap<Monitoring, BaseMonitoringResponse>();

            CreateMap<Monitoring, MonitoringWithEventsResponse>();

            CreateMap<Monitoring, MonitoringWithAccuraciesResponse>();


            CreateMap<MicroclimateEvent, MicroclimateEventResponse>();

            #endregion
        }
    }
}
