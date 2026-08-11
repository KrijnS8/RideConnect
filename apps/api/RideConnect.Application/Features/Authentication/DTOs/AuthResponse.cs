namespace RideConnect.Application.Features.Authentication.DTOs;

public record AuthResponse(
    string AccessToken,
    DateTimeOffset ExpiresAt,
    string? RefreshToken = null);
