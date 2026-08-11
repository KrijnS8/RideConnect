namespace RideConnect.Application.Features.Users.DTOs;

public sealed record UpdateUserRequest(
    string  CurrentPassword,
    string? Username,
    string? FirstName,
    string? LastName,
    string? Bio);