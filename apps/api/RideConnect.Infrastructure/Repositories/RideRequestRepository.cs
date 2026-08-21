using Microsoft.EntityFrameworkCore;
using RideConnect.Application.Persistence;
using RideConnect.Domain.Entities;
using RideConnect.Infrastructure.Persistence;

namespace RideConnect.Infrastructure.Repositories;

public sealed class RideRequestRepository(
    RideConnectDbContext dbContext) : IRideRequestRepository
{
    public async Task<RideRequest?> GetByIdAsync(Guid id)
    {
        return await dbContext.RideRequests
            .Include(x => x.Participants)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyList<RideRequest>> GetByUserIdAsync(Guid userId)
    {
        return await dbContext.RideRequests
            .Where(x => x.CreatorId == userId)
            .ToListAsync();
    }
    
    public async Task<IReadOnlyList<RideRequest>> GetAllAsync()
    {
        return await dbContext.RideRequests.ToListAsync();
    }

    public Task<bool> IsParticipantAsync(Guid userId, Guid rideRequestId)
    {
        return dbContext.RideParticipants
            .AnyAsync(x => 
                x.RideId == rideRequestId && 
                x.UserId == userId);
    }

    public async Task AddAsync(RideRequest rideRequest)
    {
        await dbContext.RideRequests.AddAsync(rideRequest);
    }

    public void Delete(RideRequest rideRequest)
    {
        dbContext.RideRequests.Remove(rideRequest);
    }
    
    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }   
}
