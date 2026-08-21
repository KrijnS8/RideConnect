namespace RideConnect.Application.Features.RideRequests.DTOs;

public record CreateRequest(
    string Title,
    string? Description,
    string Location,
    DateTimeOffset StartTime,
    int MaxParticipants);