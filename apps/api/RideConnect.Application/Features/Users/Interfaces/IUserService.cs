using RideConnect.Application.Features.Users.DTOs;
using RideConnect.Domain.Common;

namespace RideConnect.Application.Features.Users.Interfaces;

public interface IUserService
{
    Task<Result<CurrentUserResponse>> GetCurrentUserAsync(Guid userId);

    Task<Result> UpdatePasswordAsync(Guid userId, UpdatePasswordRequest req);

    Task<Result> UpdateEmailAsync(Guid userId, UpdateEmailRequest req);

    Task<Result> UpdateUserAsync(Guid userId, UpdateUserRequest req);
    
    Task<Result<PublicUserResponse>> GetPublicUserAsync(Guid userId);
    
    // Task<Result<CurrentUserResponse>> UpdateAsync(UpdateUserRequest request);
}
