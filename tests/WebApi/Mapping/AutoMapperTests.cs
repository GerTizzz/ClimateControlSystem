using AutoMapper;
using Domain.Entities;
using Shared.Dtos;
using WebApi.Mapping;

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
        public void PredictionsEntityToPredictionDto()
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Label, PredictionDto>());

            configuration.AssertConfigurationIsValid();

            Assert.Pass();
        }

        [Test]
        public void ActualDataEntityToActualDataDto()
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Fact, ActualDataDto>());

            configuration.AssertConfigurationIsValid();

            Assert.Pass();
        }
    }
}