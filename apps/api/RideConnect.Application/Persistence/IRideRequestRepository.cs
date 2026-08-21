using RideConnect.Domain.Entities;

namespace RideConnect.Application.Persistence;

public interface IRideRequestRepository
{
    Task<RideRequest?> GetByIdAsync(Guid id);
    
    // TODO: Replace with GetActiveByUserIdAsync and add history query and search query
    Task<IReadOnlyList<RideRequest>> GetAllAsync();
    
    
    Task<bool> IsParticipantAsync(Guid userId, Guid rideRequestId);
    
    Task AddAsync(RideRequest rideRequest);
    
    void Delete(RideRequest rideRequest);
    
    Task SaveChangesAsync();
}
