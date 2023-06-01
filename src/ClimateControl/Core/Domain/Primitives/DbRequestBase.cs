namespace Domain.Primitives;

public interface IDbRangeRequest
{
    int Start { get; }
    int Count { get; }
}