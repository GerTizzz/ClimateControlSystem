namespace Application.Services.Strategies;

public interface IFeaturesCollector
{
    public List<Feature> Features { get; }

    bool IsEnoughData { get; }

    void AddNewData(Feature feature);
}