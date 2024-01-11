using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/book/")]
    public class BookController : ControllerBase
    {
        public BookController() { }

        [HttpGet]
        public Task<IActionResult> GetAllBooksAsync()    // dto->pagination
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("my/")]
        public Task<IActionResult> GetMyListedBooks()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{bookId}")]
        public Task<IActionResult> GetBookByIdAsync([FromRoute] string bookId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("advanced-search/")]
        public Task<IActionResult> AdvancedBookSearch() // dto-> isbn, name, authornames[]
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<IActionResult> CreateABookAsync() // dto->isbn, name, imgurl, authorname[]
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public Task<IActionResult> ModifyBookAsync()    // dto->id, isbn, name, imgurl, authors[], status
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{bookId}")]
        public Task<IActionResult> DeleteBookAsync([FromRoute] string bookId)
        {
            throw new NotImplementedException();
        }
    }
}
