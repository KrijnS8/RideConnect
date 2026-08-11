namespace RideConnect.Application.Features.Users.DTOs;

public sealed record UpdatePasswordRequest(
    string CurrentPassword,
    string NewPassword);