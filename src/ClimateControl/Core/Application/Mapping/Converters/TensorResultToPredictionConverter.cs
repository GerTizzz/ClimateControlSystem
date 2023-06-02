using Application.Primitives;
using AutoMapper;

namespace Application.Mapping.Converters
{
    public class TensorResultToPredictionConverter : ITypeConverter<TensorResult, IEnumerable<PredictedValue>>
    {
        public IEnumerable<PredictedValue> Convert(TensorResult source, IEnumerable<PredictedValue> destination, ResolutionContext context)
        {
            foreach (var item in source.StatefulPartitionedCall.Take(TensorSettings.PredictionsCount))
            {
                yield return new PredictedValue(Guid.NewGuid())
                {
                    Value = item
                };
            }
        }
    }
}
