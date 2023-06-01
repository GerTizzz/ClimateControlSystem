namespace Shared.Dtos;

public sealed record WarningDto
{
    public float Value { get; }
    public string Message { get; }

    public WarningDto(string message, float value)
    {
        Message = message;
        Value = value;
    }
}