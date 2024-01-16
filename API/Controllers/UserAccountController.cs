using Domain.Dto;
using Domain.Exceptions;
using Domain.Services;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/user")]
    public class UserAccountController : BaseApiController
    {
        private readonly IUserAccountService _userService;

        public UserAccountController(IUserAccountService userAccountService)
        {
            _userService = userAccountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] AdvancedUserSearch advancedSearch)
        {
            try
            {
                var users = await _userService.GetAllUsersAsync(advancedSearch);
                return Ok(users);
            }
            catch (ValidationFailedException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetUserAsync([FromRoute]string userId) //redirect if id=me
        {
            try
            {
                var user = await _userService.GetSpecificUserAsync(userId);
                return Ok(user);
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
        [Route("me/")]
        public async Task<IActionResult> GetMyAccountAsync()
        {
            var uid = GetCuurentUserId();
            if (uid is null)
            {
                return Unauthorized();
            }

            try
            {
                var me = await _userService.GetCurrentUserAsync(uid);
                return Ok(me);
            }
            catch (RecordNotFoundException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCreation userCreation)    //dto-> id, name, address, email, pw
        {
            try
            {
                await _userService.CreateUserAsync(userCreation);
                return Ok();
            }
            catch (AlreadyExistsException)
            {
                return BadRequest();
            }
            catch (ValidationFailedException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> ModifyUserAsync([FromBody] UserModification userModification)    //dto-> id, name, address, email
        {
            var uid = GetCuurentUserId();
            if (uid is null)
            {
                return Unauthorized();
            }

            if(uid != userModification.UserId)
            {
                return BadRequest();
            }

            try
            {
                await _userService.ModifyUserAsync(userModification);
                return Ok();
            }
            catch (RecordNotFoundException)
            {
                return NotFound();
            }
            catch (ValidationFailedException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch]
        [Route("change-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] PasswordChange passwordChange) //dto-> old pw, new pw
        {
            var uid = GetCuurentUserId();
            if (uid is null)
            {
                return Unauthorized();
            }

            if(uid != passwordChange.UserId)
            {
                return Unauthorized();
            }

            try
            {
                await _userService.ChangePasswordAsync(passwordChange);
                return Ok();
            }
            catch (RecordNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync([FromBody] string password)    //dto->pw
        {
            var uid = GetCuurentUserId();
            if (uid is null)
            {
                return Unauthorized();
            }

            try
            {
                if(await _userService.VerifyPasswordAsync(new LoginInformation(uid, password)))
                {
                    return BadRequest();
                }

                await _userService.DeleteUserAsync(uid, password);
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
    }
}
