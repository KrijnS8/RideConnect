using RideConnect.Domain.Enums;

namespace RideConnect.Application.Features.RideRequests.DTOs;

public record RideResponse(
    Guid Id,
    string Title,
    string? Description,
    RideStatus Status,
    string? Location,
    int Participants,
    int MaxParticipants,
    DateTimeOffset CreatedAt,
    DateTimeOffset StartsAt);