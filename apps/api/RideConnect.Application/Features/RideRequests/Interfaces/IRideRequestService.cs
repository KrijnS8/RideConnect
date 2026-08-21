using RideConnect.Application.Features.RideRequests.DTOs;
using RideConnect.Domain.Common;

namespace RideConnect.Application.Features.RideRequests.Interfaces;

public interface IRideRequestService
{
    Task<Result<RideResponse>> CreateAsync(Guid userId, CreateRequest request);
    Task<Result<IReadOnlyList<RideResponse>>> GetAllAsync();
    Task<Result<RideResponse>> GetAsync(Guid id);
    // Task<Result<RideResponse>> UpdateAsync();
    Task<Result> DeleteAsync(Guid userId, Guid rideRequestId);
    
    Task<Result<RideResponse>> JoinAsync(Guid userId, Guid rideRequestId);
    Task<Result> LeaveAsync(Guid rideRequestId);
}
