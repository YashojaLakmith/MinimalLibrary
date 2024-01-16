using API.Helpers;

using Domain.Dto;
using Domain.Exceptions;
using Domain.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace API.Controllers
{
    [Route("api/v1/auth/")]
    public class AuthController : BaseApiController
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IUserAccountService _accountService;

        public AuthController(IDistributedCache distributedCache, IUserAccountService accountService)
        {
            _distributedCache = distributedCache;
            _accountService = accountService;
        }

        [HttpPost]
        [Route("login/")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginInformation loginInformation) // needs uid + password
        {
            try
            {
                if(GetCuurentUserId() is not null)
                {
                    return BadRequest();
                }

                if (Request.Cookies.TryGetValue("session", out string? sessionToken))
                {
                    return BadRequest();
                }

                if (!await _accountService.VerifyPasswordAsync(loginInformation))
                {
                    return BadRequest();
                }

                sessionToken = SessionTokenHandler.GenerarateASessionToken();
                await _distributedCache.SetStringAsync(sessionToken, loginInformation.UserId);
                Response.Cookies.Append("session", sessionToken);

                return Ok();
            }
            catch (RecordNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("logout/")]
        public async Task<IActionResult> LogoutAsync()
        {
            try
            {
                if (!Request.Cookies.TryGetValue("session", out string? sessionToken))
                {
                    return BadRequest();
                }

                var cachedToken = await _distributedCache.GetStringAsync(sessionToken);
                if (cachedToken is null)
                {
                    return BadRequest();
                }

                await _distributedCache.RemoveAsync(sessionToken);
                Response.Cookies.Delete("session");

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("req-token/{userId}")]
        public Task<IActionResult> RequestResetPasswordTokenAsync([FromRoute]string userId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("reset-password/{token}")]
        public Task<IActionResult> ResetPasswordWithTokenAsync([FromRoute]string token, [FromBody] string password)
        {
            throw new NotImplementedException();
        }
    }
}
