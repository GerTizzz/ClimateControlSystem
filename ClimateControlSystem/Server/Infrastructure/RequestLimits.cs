namespace ClimateControlSystem.Server.Infrastructure
{
    public sealed class RequestLimits
    {
        public int Start { get; set; }
        public int Count { get; set; }

        public RequestLimits()
        {

        }

        public RequestLimits(int start, int count)
        {
            Start = start;
            Count = count;
        }
    }
}
