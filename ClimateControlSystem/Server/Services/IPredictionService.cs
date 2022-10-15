namespace ClimateControlSystem.Server.Services
{
    public interface IPredictionService
    {
        float[] Predict(float[] inputData);
    }
}
