using API.Helpers;
using API.Options;

using Domain.Dto;
using Domain.Exceptions;
using Domain.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace API.Controllers
{
    [Authorize]
    [Route("api/v1/auth/")]
    public class AuthController : BaseApiController
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IUserAccountService _accountService;
        private readonly IEmailService _emailService;
        private readonly SessionCacheOptions _sessionCacheOptions;
        private readonly ResetTokenCacheOptions _resetTokenCacheOptions;
        private readonly SessionCookieOptions _sessionCookieOptions;

        public AuthController(IDistributedCache distributedCache, IUserAccountService accountService, IEmailService emailService, SessionCacheOptions sessionCacheOptions, ResetTokenCacheOptions resetTokenCacheOptions, SessionCookieOptions cookieOptions)
        {
            _distributedCache = distributedCache;
            _accountService = accountService;
            _emailService = emailService;
            _sessionCacheOptions = sessionCacheOptions;
            _resetTokenCacheOptions = resetTokenCacheOptions;
            _sessionCookieOptions = cookieOptions;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login/")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginInformation loginInformation)
        {
            try
            {
                if(GetCurrentUserId() is not null)
                {
                    return BadRequest("Already have an active session.");
                }

                if (Request.Cookies.TryGetValue(SessionCookieOptions.COOKIE_NAME, out string? sessionToken))
                {
                    return BadRequest("Already have an active session.");
                }

                if (!await _accountService.VerifyPasswordAsync(loginInformation))
                {
                    return BadRequest("Incorrect credentials.");
                }

                sessionToken = SessionTokenHandler.GenerarateASessionToken();
                await _distributedCache.SetStringAsync(sessionToken, loginInformation.UserId, _sessionCacheOptions);
                Response.Cookies.Append(SessionCookieOptions.COOKIE_NAME, sessionToken, _sessionCookieOptions);

                return Ok();
            }
            catch(ValidationFailedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message);
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
                if (!Request.Cookies.TryGetValue(SessionCookieOptions.COOKIE_NAME, out string? sessionToken))
                {
                    return BadRequest("There is no active session.");
                }

                var cachedToken = await _distributedCache.GetStringAsync(sessionToken);
                if (cachedToken is null)
                {
                    return BadRequest("There is not active session.");
                }

                await _distributedCache.RemoveAsync(sessionToken);
                Response.Cookies.Delete(SessionCookieOptions.COOKIE_NAME);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("reset-password/{userId}")]
        public async Task<IActionResult> RequestResetPasswordTokenAsync([FromRoute]string userId)
        {
            if (GetCurrentUserId() is not null)
            {
                return BadRequest("There is an active session.");
            }

            try
            {
                var url = Request.GetEncodedUrl().Split('?')[0];
                var token = SessionTokenHandler.GetARandomNumber(6);
                string subject = "Password reset request for your account.";
                string body = $"Your token for reset password for your account is:\n{token}\n Token expires in 5 minutes.";

                await _distributedCache.SetStringAsync(token.ToString(), userId);
                await _emailService.SendEmailToARegisteredUserAsync(userId, subject, body);

                return Ok();
            }
            catch(RecordNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(ValidationFailedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [AllowAnonymous]
        [HttpPatch]
        [Route("reset-password/{token}")]
        public async Task<IActionResult> ResetPasswordWithTokenAsync([FromRoute]string token, [FromBody] string password)
        {
            if(GetCurrentUserId() is not null)
            {
                return BadRequest("There is an active session.");
            }

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required");
            }

            var userId = await _distributedCache.GetStringAsync(token);
            if (userId is null)
            {
                return Unauthorized("Reset token is invalid or expired.");
            }

            try
            {
                await _accountService.ChangePasswordAsync(userId, password);

                var sessionToken = SessionTokenHandler.GenerarateASessionToken();
                await _distributedCache.RemoveAsync(token);
                await _distributedCache.SetStringAsync(sessionToken, userId);
                Response.Cookies.Append(SessionCookieOptions.COOKIE_NAME, sessionToken);

                return RedirectToAction(nameof(UserAccountController.GetMyAccountAsync));
            }
            catch(ValidationFailedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(RecordNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
