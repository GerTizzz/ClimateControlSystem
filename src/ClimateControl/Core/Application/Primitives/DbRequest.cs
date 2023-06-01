using Domain.Primitives;

namespace Application.Primitives;

public sealed class DbRangeRequest : IDbRangeRequest
{
    public int Start { get; }

    public int Count { get; }

    public DbRangeRequest(int start, int count)
    {
        Start = start;
        Count = count;
    }
}