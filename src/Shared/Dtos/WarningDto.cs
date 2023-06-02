namespace Shared.Dtos;

public sealed record WarningDto
{
    public string Message { get; }

    public WarningDto(string message)
    {
        Message = message;
    }
}