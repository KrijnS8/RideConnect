namespace RideConnect.Domain.Errors;

public enum ErrorType
{
    None,
    Validation,
    Conflict,
    NotFound,
    Authentication,
    Authorization,
    Internal
}
