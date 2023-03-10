using AutoMapper;
using Shared.Dtos;
using WebApi.Mapping;
using WebApi.Resources.Repository.TablesEntities;

namespace WebApiTests.Mapping
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
        public void MonitoringEntityToBaseMonitoringDto()
        {
            _mapper.Map<BaseMonitoringDto>(new MonitoringsEntity()
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
        public void PredictionsEntityToPredictionDto()
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<PredictionsEntity, PredictionDto>());

            configuration.AssertConfigurationIsValid();

            Assert.Pass();
        }

        [Test]
        public void ActualDataEntityToActualDataDto()
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<ActualDataEntity, ActualDataDto>());

            configuration.AssertConfigurationIsValid();

            Assert.Pass();
        }
    }
}