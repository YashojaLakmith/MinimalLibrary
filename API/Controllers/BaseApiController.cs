using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase, IContextUserExtractable
    {
        [NonAction]
        public string? GetCurrentUserId()
        {
            var uidClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            return uidClaim?.Value;
        }
    }
}
