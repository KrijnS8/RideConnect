namespace RideConnect.Application.Features.Users.DTOs;

public record UpdateEmailRequest(
    string CurrentEmail,
    string NewEmail);