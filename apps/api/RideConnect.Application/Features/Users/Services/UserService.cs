using RideConnect.Application.Features.Authentication.Interfaces;
using RideConnect.Application.Features.Users.DTOs;
using RideConnect.Application.Features.Users.Interfaces;
using RideConnect.Application.Persistence;
using RideConnect.Domain.Common;
using RideConnect.Domain.Entities;
using RideConnect.Domain.Errors;

namespace RideConnect.Application.Features.Users.Services;

public class UserService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher): IUserService
{
    public async Task<Result<CurrentUserResponse>> GetCurrentUserAsync(Guid userId)
    {
        var user = await userRepository.GetByIdAsync(userId);
        if (user is null)
            return Result<CurrentUserResponse>.Failure(UserErrors.UserNotFound);

        return Result<CurrentUserResponse>.Success(MapToCurrentUserResponse(user));
    }

    public async Task<Result> UpdatePasswordAsync(Guid userId, UpdatePasswordRequest req)
    {
        var user = await userRepository.GetByIdAsync(userId);
        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);
        
        if (!passwordHasher.Verify(user.PasswordHash, req.CurrentPassword))
            return Result.Failure(UserErrors.InvalidCredentials);
        
        user.PasswordHash = passwordHasher.Hash(req.NewPassword);

        await userRepository.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result> UpdateEmailAsync(Guid userId, UpdateEmailRequest req)
    {
        var user = await userRepository.GetByIdAsync(userId);
        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);
        
        // TODO: maybe add normilizer
        if (user.Email != req.CurrentEmail)
            return Result.Failure(UserErrors.InvalidCredentials);
        
        if (await userRepository.GetByEmailAsync(req.NewEmail) is not null)
            return Result.Failure(UserErrors.EmailTaken);
        
        user.Email = req.NewEmail;
        
        await userRepository.SaveChangesAsync();
        
        return Result.Success();   
    }

    public async Task<Result> UpdateUserAsync(Guid userId, UpdateUserRequest req)
    {
        var user = await userRepository.GetByIdAsync(userId);
        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);
        
        if (!passwordHasher.Verify(user.PasswordHash, req.CurrentPassword))
            return Result.Failure(UserErrors.InvalidCredentials);

        if (req.Username is not null && user.Username != req.Username)
        {
            if (await userRepository.GetByUsernameAsync(req.Username) is not null)
                return Result.Failure(UserErrors.UsernameTaken);
            
            user.Username = req.Username;
        }
        
        if (req.FirstName is not null)
            user.FirstName = req.FirstName;
        
        if (req.LastName is not null)
            user.LastName = req.LastName;
        
        if (req.Bio is not null)
            user.Bio = req.Bio;
        
        await userRepository.SaveChangesAsync();
        
        return Result.Success();  
    }
    
    public async Task<Result<PublicUserResponse>> GetPublicUserAsync(Guid userId)
    {
        var user = await userRepository.GetByIdAsync(userId);
        if (user is null)
            return Result<PublicUserResponse>.Failure(UserErrors.UserNotFound);
        
        return Result<PublicUserResponse>.Success(MapToPublicUserResponse(user));
    }
    
    // public Task<Result<CurrentUserResponse>> UpdateAsync(UpdateUserRequest request)
    // {
    //     throw new NotImplementedException();   
    // }

    private static CurrentUserResponse MapToCurrentUserResponse(User user) =>
        new(
            user.Id,
            user.Username,
            user.Email,
            user.FirstName,
            user.LastName,
            user.ProfilePictureUrl,
            user.Bio,
            user.CreatedAt);

    private static PublicUserResponse MapToPublicUserResponse(User user) =>
        new(
            user.Id,
            user.Username,
            user.ProfilePictureUrl,
            user.Bio,
            user.CreatedAt);
}
