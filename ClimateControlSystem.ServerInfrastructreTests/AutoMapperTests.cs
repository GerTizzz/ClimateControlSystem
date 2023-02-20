using AutoMapper;
using ClimateControlSystem.Server.Mapping;
using ClimateControlSystem.Server.Resources.Repository.TablesEntities;
using ClimateControlSystem.Shared.Responses;

namespace ClimateControlSystem.ServerInfrastructreTests
{
    public class AutoMapperTests
    {
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<AppMappingProfile>());
            _mapper = configuration.CreateMapper();
        }

        [Test]
        public void MonitoringEntityToBaseMonitoringDTO()
        {
            _mapper.Map<BaseMonitoringDTO>(new MonitoringsEntity()
            {
                Id = 0,
                Prediction = new PredictionsEntity()
                {
                    Temperature = 1,
                    Humidity = 2
                },
                ActualData = new ActualDataEntity()
                {
                    Temperature = 3,
                    Humidity = 4
                },
                TracedTime = DateTime.Now
            });

            Assert.Pass();
        }

        [Test]
        public void PredictionsEntityToPredictionDTO()
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<PredictionsEntity, PredictionsDTO>());

            configuration.AssertConfigurationIsValid();

            Assert.Pass();
        }

        [Test]
        public void ActualDataEntityToActualDataDTO()
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<ActualDataEntity, ActualDataDTO>());

            configuration.AssertConfigurationIsValid();

            Assert.Pass();
        }
    }
}