using RideConnect.Domain.Enums;

namespace RideConnect.Application.Features.RideRequests.DTOs;

public record RideResponse(
    Guid Id,
    string Title,
    string? Description,
    RideStatus Status,
    string? Location,
    int MaxParticipants,
    DateTimeOffset CreatedAt);