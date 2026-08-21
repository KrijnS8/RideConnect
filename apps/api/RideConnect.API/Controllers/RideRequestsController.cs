using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RideConnect.API.Extensions;
using RideConnect.Application.Features.RideRequests.DTOs;
using RideConnect.Application.Features.RideRequests.Interfaces;

namespace RideConnect.API.Controllers;

[ApiController]
[Route("api/ride-requests")]
public class RideRequestsController(
    IRideRequestService rideRequestService): ControllerBase
{
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateRideRequest(CreateRequest request)
    {
        if (!User.TryGetUserId(out var userId))
            return Unauthorized();
        
        var response = await rideRequestService.CreateAsync(userId, request);
        
        return response.ToActionResult();
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetRideRequests()
    {
        if (!User.TryGetUserId(out var userId))
            return Unauthorized();

        var response = await rideRequestService.GetAllAsync();
        
        return response.ToActionResult();
    }
    
    [Authorize]
    [HttpGet("{rideRequestId:guid}")]
    public async Task<IActionResult> GetRideRequestById(Guid rideRequestId)
    {
        if (!User.TryGetUserId(out var userId))
            return Unauthorized();

        var response = await rideRequestService.GetAsync(rideRequestId);
        
        return response.ToActionResult();
    }
    
    [Authorize]
    [HttpPut("{rideRequestId:guid}")]
    public Task<IActionResult> UpdateRideRequest(Guid rideRequestId)
    {
        throw new NotImplementedException();
    }
    
    // [Authorize]
    // [HttpDelete("{rideRequestId:guid}")]
    // public Task<IActionResult> DeleteRideRequest(Guid rideRequestId)
    // {
    //     throw new NotImplementedException();
    // }
    //
    //-------------------------------
    // Participant Endpoints
    //-------------------------------
    
    [Authorize]
    [HttpPost("{rideRequestId:guid}/participants")]
    public async Task<IActionResult> JoinRide(Guid rideRequestId)
    {
        if (!User.TryGetUserId(out var userId))
            return Unauthorized();
        
        var response = await rideRequestService.JoinAsync(userId, rideRequestId);
        
        return response.ToActionResult();
    }
    
    [Authorize]
    [HttpDelete("{rideRequestId:guid}/participants/me")]
    public Task<IActionResult> LeaveRide(Guid rideRequestId)
    {
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpGet("{rideRequestId:guid}/participants")]
    public Task<IActionResult> GetRideParticipants(Guid rideRequestId)
    {
        throw new NotImplementedException();   
    }
}
