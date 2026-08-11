namespace RideConnect.Application.Features.Authentication.DTOs;

public sealed record RegisterRequest(
    string Username,
    string Email,
    string Password,
    string ConfirmPassword);
