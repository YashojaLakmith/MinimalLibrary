using Domain.Dto;
using Domain.Services;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserAccountController : ControllerBase
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
            catch (Exception)
            {
                throw new NotImplementedException();
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
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        [HttpGet]
        [Route("me/")]
        public async Task<IActionResult> GetMyAccountAsync()
        {
            try
            {
                var me = await _userService.GetCurrentUserAsync("");
                return Ok(me);
            }
            catch (Exception)
            {
                throw new NotImplementedException();
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
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        [HttpPatch]
        public async Task<IActionResult> ModifyUserAsync([FromBody] UserModification userModification)    //dto-> name, address, email
        {
            try
            {
                await _userService.ModifyUserAsync(userModification);
                return Ok();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        [HttpPatch]
        [Route("change-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] PasswordChange passwordChange) //dto-> old pw, new pw
        {
            try
            {
                await _userService.ChangePasswordAsync(passwordChange);
                return Ok();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync([FromBody] string password)    //dto->pw
        {
            try
            {
                await _userService.DeleteUserAsync(password);
                return Ok();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}
