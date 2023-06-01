namespace Shared.Dtos;

public sealed record PredictionDto
{
    public float Value { get; set; }
    public WarningDto? Warning { get; init; }
}