using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserAccountController : ControllerBase
    {
        public UserAccountController() { }

        [HttpGet]
        public Task<IActionResult> GetAllAsync() //need pagination
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{userId}")]
        public Task<IActionResult> GetUserAsync([FromRoute]string userId) //redirect if id=me
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("me/")]
        public Task<IActionResult> GetMyAccountAsync()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("advaced-search/")]
        public Task<IActionResult> AdvancedUserSearchAsync()    //search query-> address, name, bookname, bookId, pagination
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<IActionResult> CreateUserAsync()    //dto-> id, name, address, email, pw
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public Task<IActionResult> ModifyUserAsync()    //dto-> name, address, email
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        [Route("change-password")]
        public Task<IActionResult> ChangePasswordAsync() //dto-> old pw, new pw
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public Task<IActionResult> DeleteUserAsync()    //dto->pw
        {
            throw new NotImplementedException();
        }
    }
}
