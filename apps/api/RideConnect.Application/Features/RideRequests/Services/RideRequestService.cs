using RideConnect.Application.Features.RideRequests.DTOs;
using RideConnect.Application.Features.RideRequests.Interfaces;
using RideConnect.Application.Persistence;
using RideConnect.Domain.Common;
using RideConnect.Domain.Entities;
using RideConnect.Domain.Enums;
using RideConnect.Domain.Errors;

namespace RideConnect.Application.Features.RideRequests.Services;

public sealed class RideRequestService(
    IRideRequestRepository rideRequestRepository,
    IUserRepository userRepository): IRideRequestService
{
    // TODO: Add start time to create request and ride request
    public async Task<Result<RideResponse>> CreateAsync(Guid userId, CreateRequest request)
    {
        var creator = await userRepository.GetByIdAsync(userId);
        if (creator is null)
            return Result<RideResponse>.Failure(UserErrors.NotFound);

        var ride = new RideRequest
        {
            CreatorId = userId,
            Title = request.Title,
            Description = request.Description,
            Status = RideStatus.Open,
            Location = request.Location,
            MaxParticipants = request.MaxParticipants,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
            StartsAt = request.StartTime
        };

        ride.Participants.Add(new RideParticipant
        {
            UserId = userId,
            Role = RideRole.Organizer,
            JoinedAt = DateTimeOffset.UtcNow
        });

        await rideRequestRepository.AddAsync(ride);
        await rideRequestRepository.SaveChangesAsync();

        return Result<RideResponse>.Success(MapToRideResponse(ride));
    }

    public async Task<Result<IReadOnlyList<RideResponse>>> GetAllAsync()
    {
        var rides = await rideRequestRepository.GetAllAsync();
        
        var responses = rides.Select(MapToRideResponse).ToList();
        
        return Result<IReadOnlyList<RideResponse>>.Success(responses);
    }

    public async Task<Result<RideResponse>> GetAsync(Guid id)
    {
        var ride = await rideRequestRepository.GetByIdAsync(id);
        if (ride is null)
            return Result<RideResponse>.Failure(RideRequestErrors.NotFound);

        return Result<RideResponse>.Success(MapToRideResponse(ride));
    }

    public async Task<Result> DeleteAsync(Guid userId, Guid rideRequestId)
    {
        var ride = await rideRequestRepository.GetByIdAsync(rideRequestId);
        if (ride is null)
            return Result.Failure(RideRequestErrors.NotFound);

        if (ride.CreatorId != userId)
            return Result.Failure(RideRequestErrors.NotCreator);
        
        rideRequestRepository.Delete(ride);
        await rideRequestRepository.SaveChangesAsync();
        
        return Result.Success();   
    }

    // TODO: Check for race condition when 2 users join at the same time
    public async Task<Result<RideResponse>> JoinAsync(Guid userId, Guid rideRequestId)
    {
        var user = await userRepository.GetByIdAsync(userId);
        if (user is null)
            return Result<RideResponse>.Failure(UserErrors.NotFound);

        var ride = await rideRequestRepository.GetByIdAsync(rideRequestId);
        if (ride is null)
            return Result<RideResponse>.Failure(RideRequestErrors.NotFound);

        if (await rideRequestRepository.IsParticipantAsync(userId, rideRequestId))
            return Result<RideResponse>.Failure(RideRequestErrors.AlreadyJoined);
        
        if (ride.Status != RideStatus.Open)
            return Result<RideResponse>.Failure(RideRequestErrors.NotOpen);

        if (ride.Participants.Count >= ride.MaxParticipants)
            return Result<RideResponse>.Failure(RideRequestErrors.MaxParticipants);
        
        ride.Participants.Add(new RideParticipant
        {
            UserId = userId,
            Role = RideRole.Member,
            JoinedAt = DateTimeOffset.UtcNow
        });
        
        await rideRequestRepository.SaveChangesAsync();
        
        return Result<RideResponse>.Success(MapToRideResponse(ride));
    }

    public Task<Result> LeaveAsync(Guid rideRequestId)
    {
        throw new NotImplementedException();
    }

    private static RideResponse MapToRideResponse(RideRequest ride) =>
        new(
            ride.Id,
            ride.Title,
            ride.Description,
            ride.Status,
            ride.Location,
            ride.Participants.Count,
            ride.MaxParticipants,
            ride.CreatedAt,
            ride.StartsAt);
}
