namespace RideConnect.Domain.Errors;

public class RideRequestErrors
{
    public static readonly Error NotFound = 
        new("NOT_FOUND", "Ride request not found", ErrorType.NotFound);
    
    public static readonly Error NotCreator = 
        new("NOT_CREATOR", "You are not the creator of this ride request", ErrorType.Authorization);
    
    public static readonly Error AlreadyJoined = 
        new("ALREADY_JOINED", "You have already joined this ride request", ErrorType.Conflict);
    
    public static readonly Error NotOpen = 
        new("NOT_OPEN", "Ride request is not open", ErrorType.Conflict);
    
    public static readonly Error MaxParticipants = 
        new("MAX_PARTICIPANTS", "Ride request has reached its maximum number of participants", ErrorType.Conflict);
}
