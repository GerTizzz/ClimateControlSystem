﻿using AutoMapper;
using ClimateControlSystem.Server.Protos;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Resources.RepositoryResources;
using ClimateControlSystem.Server.Services.PredictionEngine.PredictionEngineResources;
using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Server.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            #region gRPC communication

            CreateMap<ClimateMonitoringRequest, IncomingMonitoringData>()
                .ForMember(data => data.MeasurementTime, request => request
                    .MapFrom(requestSrc => requestSrc.MeasurementTime.ToDateTimeOffset().ToLocalTime()));

            #endregion

            #region PredictEngine

            CreateMap<IncomingMonitoringData, TensorPredictionRequest>()
                .ForMember(tensor => tensor.serving_default_input_1, opt => opt
                    .MapFrom(property => new float[]
                    {
                        property.ClusterLoad,
                        property.CpuUsage,
                        property.ClusterTemperature,
                        property.PreviousTemperature,
                        property.PreviousHumidity,
                        property.AirHumidityOutside,
                        property.AirDryTemperatureOutside,
                        property.AirWetTemperatureOutside,
                        property.WindSpeed,
                        property.WindDirection,
                        property.WindEnthalpy,
                        property.MeanCoolingValue
                    }));

            CreateMap<TensorPredictionResult, PredictionResult>()
                .ForMember(result => result.PredictedTemperature, tensor => tensor
                    .MapFrom(tensorSrc => tensorSrc.StatefulPartitionedCall[0]))
                .ForMember(result => result.PredictedHumidity, tensor => tensor
                    .MapFrom(tensorSrc => tensorSrc.StatefulPartitionedCall[1]));

            #endregion

            #region PredictionService

            CreateMap<IncomingMonitoringData, MonitoringData>();

            #endregion
            
            #region Repository

            CreateMap<MonitoringData, MonitoringRecord>();

            CreateMap<MonitoringRecord, MonitoringData>();

            CreateMap<MonitoringRecord, PredictionResult>();

            #endregion

            CreateMap<UserRecord, UserDtoModel>()
                .ForMember(dto => dto.Name, auth => auth
                    .MapFrom(authSrc => authSrc.Name))
                .ForMember(dto => dto.Role, auth => auth
                    .MapFrom(authSrc => authSrc.Role))
                .ForMember(dto => dto.Id, auth => auth
                    .MapFrom(authSrc => authSrc.Id));

            CreateMap<UserDtoModel, UserRecord>()
                .ForMember(auth => auth.Name, dto => dto
                    .MapFrom(dtoSrc => dtoSrc.Name))
                .ForMember(auth => auth.Role, dto => dto
                    .MapFrom(dtoSrc => dtoSrc.Role));
        }
    }
}
