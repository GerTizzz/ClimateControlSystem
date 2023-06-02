namespace Application.Primitives
{
    public static class TensorSettings
    {
        public const int NumberOfDataSets = 144;
        public const int MeasurementsPerDataSet = 3;
        public const int PredictionsCount = 6;

        public static int InputSize => NumberOfDataSets * MeasurementsPerDataSet;
    }
}
