using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/borrowings/")]
    public class BookBorrowingController : ControllerBase
    {
        public BookBorrowingController() { }

        [HttpGet]
        [Route("in/")]
        public Task<IActionResult> GetBooksBorrowedByMeAsync()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("away/")]
        public Task<IActionResult> GetBooksBorrowedFromMeAsync()
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        [Route("borrow/")]
        public Task<IActionResult> BorrowABook()    // dto-> userid, bookid
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        [Route("return/")]
        public Task<IActionResult> ReturnABookAsync()   // dto-> userid, bookid
        {
            throw new NotImplementedException();
        }
    }
}
