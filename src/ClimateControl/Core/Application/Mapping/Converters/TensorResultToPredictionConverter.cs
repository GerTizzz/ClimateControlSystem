using Application.Primitives;
using AutoMapper;

namespace Application.Mapping.Converters
{
    public class TensorResultToPredictionConverter : ITypeConverter<TensorPredictionResult, IEnumerable<PredictedValue>>
    {
        public IEnumerable<PredictedValue> Convert(TensorPredictionResult source, IEnumerable<PredictedValue> destination, ResolutionContext context)
        {
            foreach (var item in source.StatefulPartitionedCall)
            {
                yield return new PredictedValue(Guid.NewGuid())
                {
                    Value = item
                };
            }
        }
    }
}
