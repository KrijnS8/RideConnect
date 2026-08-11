namespace RideConnect.Domain.Errors;

public record Error(
    string Code,
    string Description,
    ErrorType Type)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.None);
    
    public static readonly Error InternalError = new("INTERNAL_ERROR", "Something went wrong", ErrorType.Internal);
}
