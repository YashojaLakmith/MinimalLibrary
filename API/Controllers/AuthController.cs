using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/auth/")]
    public class AuthController : ControllerBase
    {
        public AuthController() { }

        [HttpPost]
        [Route("login/")]
        public Task<IActionResult> LoginAsync() // needs uid + password
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("logout/")]
        public Task<IActionResult> LogoutAsync()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("req-token/{userId}")]
        public Task<IActionResult> RequestResetPasswordTokenAsync([FromRoute]string userId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("reset-password/{token}")]
        public Task<IActionResult> ResetPasswordWithToken([FromRoute]string token)
        {
            throw new NotImplementedException();
        }
    }
}
