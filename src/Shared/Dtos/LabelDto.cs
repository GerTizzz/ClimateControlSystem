namespace Shared.Dtos;

public sealed record LabelDto
{
    public float Temperature { get; set; }
    public float Humidity { get; set; }
}