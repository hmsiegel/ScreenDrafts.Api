namespace Presentation.Abstractions;

#pragma warning disable SA1401 // Fields should be private
[ApiController]
public class BaseApiController : ControllerBase
{
    protected readonly ISender Sender;
    protected readonly IMapper Mapper;

    protected BaseApiController(ISender sender, IMapper mapper)
    {
        Sender = sender;
        Mapper = mapper;
    }

    protected IActionResult HandleFailure(Result result)
    {
        return result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            _ =>
                BadRequest(
                    CreateProblemDetails(
                        "Bad request",
                        "Bad request",
                        "One or more errors occurred",
                        StatusCodes.Status400BadRequest,
                        result.Errors))
        };
    }

    private static ProblemDetails CreateProblemDetails(
        string title,
        string type,
        string detail,
        int status,
        Error[]? errors = null)
    {
        return new()
        {
            Title = title,
            Type = type,
            Detail = detail,
            Status = status,
            Extensions =
            {
                { nameof(errors), errors },
            },
        };
    }
}
