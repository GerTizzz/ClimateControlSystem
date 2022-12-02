namespace ClimateControlSystem.Client.Resources
{
    public record GraphicData<T,K>
    {
        public T X { get; }

        public K Y { get; }

        public string Type { get; }

        public GraphicData(T x, K y, string type)
        {
            X = x;
            Y = y;
            Type = type;
        }
    }
}
