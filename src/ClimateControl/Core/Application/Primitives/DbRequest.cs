using Domain.Primitives;

namespace Application.Primitives;

public sealed class DbRequest : IDbRequest
{
    public int Start { get; }

    public int Count { get; }

    public DbRequest(int start, int count)
    {
        Start = start;
        Count = count;
    }
}