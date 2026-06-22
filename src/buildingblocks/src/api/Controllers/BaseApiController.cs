using System;
using System.Collections.Generic;
using System.Text;
using BuildingBlocks.Common;
using BuildingBlocks.Common.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlocks.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected IActionResult Get<T>(Result<T> result) => HandleResult(result);
    
    protected IActionResult Create<T>(Result<T> result) => HandleCreatedResult(result, null, null);
    
    protected IActionResult Create<T>(
        Result<T> result,
        string actionName = "",
        object? routeValues = null) => HandleCreatedResult(result, actionName, routeValues);

    protected IActionResult Update<T>(Result<T> result) => HandleResult(result);

    protected IActionResult Delete<T>(Result<T> result) => HandleResult(result);

    private IActionResult HandleResult<T>(Result<T> result)
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
    private IActionResult HandleCreatedResult<T>(
      Result<T> result,
      string? actionName = null,
      object? routeValues = null)
    {
        if (result.IsSuccess)
            return actionName is not null
                ? CreatedAtAction(actionName, routeValues, result.Value)
                : StatusCode(StatusCodes.Status201Created, result.Value);

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