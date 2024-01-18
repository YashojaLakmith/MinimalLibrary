using API.Helpers;
using API.Options;

using Domain.Dto;
using Domain.Exceptions;
using Domain.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace API.Controllers
{
    [Authorize]
    [Route("api/v1/user")]
    public class UserAccountController : BaseApiController
    {
        private readonly IUserAccountService _userService;
        private readonly IDistributedCache _distributedCache;
        private readonly SessionCacheOptions _sessionCacheOptions;
        private readonly SessionCookieOptions _sessionCookieOptions;

        public UserAccountController(IUserAccountService userAccountService, IDistributedCache distributedCache, SessionCacheOptions sessionCacheOptions, SessionCookieOptions cookieOptions)
        {
            _userService = userAccountService;
            _distributedCache = distributedCache;
            _sessionCacheOptions = sessionCacheOptions;
            _sessionCookieOptions = cookieOptions;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] AdvancedUserSearch advancedSearch)
        {
            try
            {
                var users = await _userService.GetAllUsersAsync(advancedSearch);
                return Ok(users);
            }
            catch (ValidationFailedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetUserAsync([FromRoute]string userId)
        {
            try
            {
                var user = await _userService.GetSpecificUserAsync(userId);
                return Ok(user);
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
        [Route("me/")]
        public async Task<IActionResult> GetMyAccountAsync()
        {
            var uid = GetCurrentUserId();
            if (uid is null)
            {
                return Unauthorized("There is not active session.");
            }

            try
            {
                var me = await _userService.GetCurrentUserAsync(uid);
                return Ok(me);
            }
            catch (RecordNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCreation userCreation)
        {
            if(GetCurrentUserId() is not null)
            {
                return BadRequest("There is an active session.");
            }

            try
            {
                await _userService.CreateUserAsync(userCreation);
                var sessionToken = SessionTokenHandler.GenerarateASessionToken();

                await _distributedCache.SetStringAsync(sessionToken, userCreation.UserId, _sessionCacheOptions);
                Response.Cookies.Append(SessionCookieOptions.COOKIE_NAME, sessionToken, _sessionCookieOptions);

                return CreatedAtAction(nameof(GetMyAccountAsync), null, null);
            }
            catch (AlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ValidationFailedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> ModifyUserAsync([FromBody] UserModification userModification)
        {
            var uid = GetCurrentUserId();
            if (uid is null)
            {
                return Unauthorized("There is not active session.");
            }

            if(uid != userModification.UserId)
            {
                return BadRequest("Given user information does not match with the logged in user.");
            }

            try
            {
                await _userService.ModifyUserAsync(userModification);
                return Ok();
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationFailedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch]
        [Route("change-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] PasswordChange passwordChange)
        {
            var uid = GetCurrentUserId();
            if (uid is null)
            {
                return Unauthorized("There is not active session.");
            }

            if(uid != passwordChange.UserId)
            {
                return Unauthorized("Given user information does not match with the logged in user.");
            }

            try
            {
                await _userService.ChangePasswordAsync(passwordChange);
                return Ok();
            }
            catch (ValidationFailedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(AuthenticationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync([FromBody] string password)
        {
            var uid = GetCurrentUserId();
            if (uid is null)
            {
                return Unauthorized("There is not active session.");
            }

            if (!Request.Cookies.TryGetValue(SessionCookieOptions.COOKIE_NAME, out string? sessionId))
            {
                return Unauthorized("There is not active session.");
            }

            try
            {                
                if(await _userService.VerifyPasswordAsync(new LoginInformation(uid, password)))
                {
                    return BadRequest("Incorrect credentials.");
                }

                await _userService.DeleteUserAsync(uid, password);
                await _distributedCache.RemoveAsync(sessionId);
                Response.Cookies.Delete(sessionId);

                return Ok();
            }
            catch (ValidationFailedException ex)
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
    }
}
