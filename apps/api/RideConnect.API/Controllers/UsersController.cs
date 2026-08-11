
// GET api/users/{user.id}
// GET api/users/me
// PUT api/users/me

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RideConnect.API.Extensions;
using RideConnect.Application.Features.Users.DTOs;
using RideConnect.Application.Features.Users.Interfaces;

namespace RideConnect.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUserService userService) : ControllerBase
{
    // Get Current User Profile
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        if (!User.TryGetUserId(out var userId))
            return Unauthorized();
        
        var response = await userService.GetCurrentUserAsync(userId);
        
        return response.ToActionResult();
    }

    // Update User Password
    [Authorize]
    [HttpPut("me/password")]
    public async Task<IActionResult> UpdatePassword(UpdatePasswordRequest request)
    {
        if (!User.TryGetUserId(out var userId))
            return Unauthorized();

        var response = await userService.UpdatePasswordAsync(userId, request);
        
        return response.ToActionResult();
    }

    // Update User Email
    [Authorize]
    [HttpPut("me/email")]
    public async Task<IActionResult> UpdateEmail(UpdateEmailRequest request)
    {
        if (!User.TryGetUserId(out var userId))
            return Unauthorized();
        
        var response = await userService.UpdateEmailAsync(userId, request);
        
        return response.ToActionResult();
    }

    // Update User Profile
    [Authorize]
    [HttpPut("me")]
    public async Task<IActionResult> UpdateMe(UpdateUserRequest request)
    {
        if (!User.TryGetUserId(out var userId))
            return Unauthorized();
        
        var response = await userService.UpdateUserAsync(userId, request);
        
        return response.ToActionResult();
    }
    
    // Get User Profile
    [HttpGet("{userId:guid}")]
    public Task<IActionResult> GetById(Guid userId)
    {
        throw new NotImplementedException();
    }
}
