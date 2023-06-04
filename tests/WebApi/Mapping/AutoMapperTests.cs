using Application.Mapping;
using Application.Primitives;
using AutoMapper;
using Domain.Entities;
using Shared.Dtos;

namespace ApplicationTests.Mapping
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
            var feature = new Feature(Guid.NewGuid(), 23f, 22f, 21f);

            var tensor = _mapper.Map<TensorRequest>(feature);

            Assert.Pass();
        }
    }
}