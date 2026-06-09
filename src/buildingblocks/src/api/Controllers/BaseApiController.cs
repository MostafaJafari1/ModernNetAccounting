using System;
using System.Collections.Generic;
using System.Text;
using BuildingBlocks.Application.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlocks.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Type switch
        {
            ErrorType.NotFound => NotFound(ToProblemDetails(result.Error)),
            ErrorType.Validation => BadRequest(ToProblemDetails(result.Error)),
            ErrorType.Unauthorized => Unauthorized(ToProblemDetails(result.Error)),
            ErrorType.Conflict => Conflict(ToProblemDetails(result.Error)),
            _ => StatusCode(500, ToProblemDetails(result.Error))
        };
    }

    protected IActionResult HandleCreatedResult<T>(
        Result<T> result,
        string actionName,
        object routeValues)
    {
        if (result.IsSuccess)
            return CreatedAtAction(actionName, routeValues, result.Value);

        return result.Error.Type switch
        {
            ErrorType.Validation => BadRequest(ToProblemDetails(result.Error)),
            ErrorType.Conflict => Conflict(ToProblemDetails(result.Error)),
            _ => StatusCode(500, ToProblemDetails(result.Error))
        };
    }

    #region Private Methodes
    private ProblemDetails ToProblemDetails(Error error)
    {
        return new ProblemDetails
        {
            Title = GetTitle(error.Type),
            Detail = error.Message,
            Status = GetStatusCode(error.Type),
            Extensions = { ["errorCode"] = error.Code }
        };
    }
    private static string GetTitle(ErrorType type) => type switch
    {
        ErrorType.NotFound => "Resource Not Found",
        ErrorType.Validation => "Validation Failed",
        ErrorType.Unauthorized => "Unauthorized",
        ErrorType.Conflict => "Conflict",
        _ => "Internal Server Error"
    };
    private static int GetStatusCode(ErrorType type) => type switch
    {
        ErrorType.NotFound => StatusCodes.Status404NotFound,
        ErrorType.Validation => StatusCodes.Status400BadRequest,
        ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
        ErrorType.Conflict => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status500InternalServerError
    }; 
    #endregion
}