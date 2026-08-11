using RideConnect.Application.Features.Authentication.DTOs;
using RideConnect.Application.Features.Authentication.Interfaces;
using RideConnect.Application.Persistence;
using RideConnect.Domain.Common;
using RideConnect.Domain.Entities;
using RideConnect.Domain.Errors;

namespace RideConnect.Application.Features.Authentication.Services;

public sealed class AuthService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator
    ) : IAuthService
{
    public async Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request)
    {
        // check if email exists
        if (await userRepository.GetByEmailAsync(request.Email) is not null)
            return Result<AuthResponse>.Failure(UserErrors.EmailTaken);
        
        // check if username exists
        if (await userRepository.GetByUsernameAsync(request.Username) is not null)
            return Result<AuthResponse>.Failure(UserErrors.UsernameTaken);
        
        // create user
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            
            PasswordHash = passwordHasher.Hash(request.Password),
            
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
            
            IsVerified = false,
            IsActive = true
        };

        await userRepository.AddAsync(user);
        
        // save user
        await userRepository.SaveChangesAsync();
        
        // send verification email
        
        // generate JWT
        var token = jwtTokenGenerator.Generate(user);
        
        // return token
        // change to refresh token
        var response = new AuthResponse(token.AccessToken, token.ExpiresAt);

        return Result<AuthResponse>.Success(response);
    }

    public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request)
    {
        var user = request.Login.Contains('@')
            ? await userRepository.GetByEmailAsync(request.Login)
            : await userRepository.GetByUsernameAsync(request.Login);

        if (user is null)
            return Result<AuthResponse>.Failure(UserErrors.InvalidCredentials);

        if (!passwordHasher.Verify(user.PasswordHash, request.Password))
            return Result<AuthResponse>.Failure(UserErrors.InvalidCredentials);
        
        // generate JWT
        var token = jwtTokenGenerator.Generate(user);
        
        // return token
        // change to refresh token
        var response = new AuthResponse(token.AccessToken, token.ExpiresAt);
        return Result<AuthResponse>.Success(response);
    }
}
