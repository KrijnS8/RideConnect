using Microsoft.AspNetCore.Mvc;
using RideConnect.Domain.Common;
using RideConnect.Domain.Errors;

namespace RideConnect.API.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult(
        this Result result)
    {
        if (result.IsSuccess)
            return new NoContentResult();
        
        return ToErrorResult(result.Error);
    }
    
    public static IActionResult ToActionResult<T>(
        this Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        return ToErrorResult(result.Error);
    }

    private static IActionResult ToErrorResult(Error error)
    {
        return error.Type switch
        {
            ErrorType.Validation => new BadRequestObjectResult(error),
            ErrorType.Conflict => new ConflictObjectResult(error),
            ErrorType.NotFound => new NotFoundObjectResult(error),
            ErrorType.Authentication => new UnauthorizedObjectResult(error),
            ErrorType.Authorization => new ObjectResult(error)
            {
                StatusCode = StatusCodes.Status403Forbidden
            },
            _ => throw new InvalidOperationException(
                $"Unhandled error type: {error.Type}.")
        };
    }
}
