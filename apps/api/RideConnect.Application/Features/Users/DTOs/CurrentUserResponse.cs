namespace RideConnect.Application.Features.Users.DTOs;

public record CurrentUserResponse(
    Guid Id,
    string Username,
    string Email,
    string? FirstName,
    string? LastName,
    string? ProfilePictureUrl,
    string? Bio,
    DateTimeOffset CreatedAt);
