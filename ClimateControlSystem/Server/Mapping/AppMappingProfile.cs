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

            CreateMap<ClimateMonitoringRequest, IncomingMonitoringData>();

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

            CreateMap<IncomingMonitoringData, MonitoringData>()
                .ForMember(data => data.MeasurementTime, incoming => incoming
                    .MapFrom(incomingSrc => new DateTime(incomingSrc.MeasurementTimeTicks)));

        #endregion
            
            #region Repository

            CreateMap<MonitoringData, MonitoringDataRecord>();

            CreateMap<MonitoringDataRecord, MonitoringData>()
                .ForMember(data => data.MeasurementTime, record => record
                    .MapFrom(recordSrc => new DateTime(recordSrc.MeasurementTimeTicks)));

            CreateMap<MonitoringDataRecord, PredictionResult>();

            #endregion
        }
    }
}
