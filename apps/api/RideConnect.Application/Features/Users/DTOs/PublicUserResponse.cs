namespace RideConnect.Application.Features.Users.DTOs;

public record PublicUserResponse(
    Guid Id,
    string Username, 
    string? ProfilePictureUrl,
    string? Bio,
    DateTimeOffset CreatedAt);
