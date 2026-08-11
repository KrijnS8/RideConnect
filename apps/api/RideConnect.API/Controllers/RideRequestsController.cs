using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RideConnect.API.Controllers;

[ApiController]
[Route("api/ride-requests")]
public class RideRequestsController : ControllerBase
{
    [Authorize]
    [HttpPost]
    public Task<IActionResult> CreateRideRequest()
    {
        throw new NotImplementedException();
    }
    
    [Authorize]
    [HttpGet]
    public Task<IActionResult> GetRideRequests()
    {
        throw new NotImplementedException();
    }
    
    [Authorize]
    [HttpGet("{rideRequestId:guid}")]
    public Task<IActionResult> GetRideRequestById(Guid rideRequestId)
    {
        throw new NotImplementedException();
    }
    
    [Authorize]
    [HttpPut("{rideRequestId:guid}")]
    public Task<IActionResult> UpdateRideRequest(Guid rideRequestId)
    {
        throw new NotImplementedException();
    }
    
    [Authorize]
    [HttpDelete("{rideRequestId:guid}")]
    public Task<IActionResult> DeleteRideRequest(Guid rideRequestId)
    {
        throw new NotImplementedException();
    }
    
    //-------------------------------
    // Participant Endpoints
    //-------------------------------
    
    [Authorize]
    [HttpPost("{rideRequestId:guid}/participants")]
    public Task<IActionResult> JoinRide(Guid rideRequestId)
    {
        throw new NotImplementedException();
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
