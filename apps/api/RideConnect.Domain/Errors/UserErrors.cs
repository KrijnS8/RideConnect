namespace RideConnect.Domain.Errors;

public class UserErrors
{
    public static Error InvalidCredentials => new("INVALID_CREDENTIALS", "Invalid credentials", ErrorType.Authentication);
    public static Error UsernameTaken => new("USERNAME_TAKEN", "Username already taken", ErrorType.Conflict);
    public static Error EmailTaken => new("EMAIL_TAKEN", "Email already taken", ErrorType.Conflict);
    public static Error UserNotFound => new("INVALID_USER", "Something went wrong", ErrorType.Authentication);
}
