using Domain.Primitives;

namespace Application.Primitives
{
    public sealed class DbRequest : IDbRequest
    {
        public int Start { get; private set; }

        public int Count { get; private set; }

        public DbRequest(int start, int count)
        {
            Start = start;
            Count = count;
        }
    }
}
