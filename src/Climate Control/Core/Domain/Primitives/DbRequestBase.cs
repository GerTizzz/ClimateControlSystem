namespace Domain.Primitives
{
    public interface IDbRequest
    {
        int Start { get; }
        int Count { get; }
    }
}
