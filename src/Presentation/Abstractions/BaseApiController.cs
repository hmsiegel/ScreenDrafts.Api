namespace Presentation.Abstractions;

[ApiController]
public class BaseApiController : ControllerBase
{
    private ISender _sender = null!;
    private IMapper _mapper = null!;

    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();

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
