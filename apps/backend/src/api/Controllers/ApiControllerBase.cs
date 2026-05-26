using api.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    protected ActionResult HandleError(Result result)
    {
        return StatusCode(result.StatusCode, new { message = result.Error });
    }

    protected ActionResult<T> HandleError<T>(Result<T> result)
    {
        return StatusCode(result.StatusCode, new { message = result.Error });
    }
}
