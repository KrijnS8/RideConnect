namespace RideConnect.Application.Features.Authentication.DTOs;

public record LoginRequest(
    string Login,
    string Password);
