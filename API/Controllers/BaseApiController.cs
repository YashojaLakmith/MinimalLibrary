using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        [NonAction]
        protected string? GetCuurentUserId()
        {
            var uidClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            return uidClaim?.Value;
        }
    }
}
