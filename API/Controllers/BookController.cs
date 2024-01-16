using Domain.Dto;
using Domain.Exceptions;
using Domain.Services;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/book/")]
    public class BookController : BaseApiController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync([FromQuery] Pagination pagination, [FromBody] AdvancedBookSearch advancedSearch)    // dto->pagination
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync(pagination, advancedSearch);
                return Ok(books);
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
        [Route("my/")]
        public async Task<IActionResult> GetMyListedBooks([FromQuery] Pagination pagination)
        {
            var id = GetCuurentUserId();

            if(id is null)
            {
                return Unauthorized();
            }

            try
            {
                var books = await _bookService.GetListingsOfCurrentUserAsync(pagination, id);
                return Ok(books);
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

        [HttpGet]
        [Route("{bookId}")]
        public async Task<IActionResult> GetBookByIdAsync([FromRoute] string bookId)
        {
            try
            {
                var book = await _bookService.GetSpecificBookByIdAsync(bookId);
                return Ok(book);
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

        [HttpPost]
        public async Task<IActionResult> CreateABookAsync([FromBody] BookCreate bookCreate) // dto->isbn, name, imgurl, authorname[]
        {
            var uid = GetCuurentUserId();
            if (uid is null)
            {
                return Unauthorized();
            }

            try
            {
                await _bookService.HandleCreateBookAsync(bookCreate, uid);
                return Created();
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
        public async Task<IActionResult> ModifyBookAsync([FromBody] ModifyBook modifyBook)    // dto->id, isbn, name, imgurl, authors[], status
        {
            var uid = GetCuurentUserId();
            if (uid is null)
            {
                return Unauthorized();
            }

            try
            {
                await _bookService.HandleModifyBookAsync(modifyBook, uid);
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

        [HttpDelete]
        [Route("{bookId}")]
        public async Task<IActionResult> DeleteBookAsync([FromRoute] string bookId)
        {
            var uid = GetCuurentUserId();
            if (uid is null)
            {
                return Unauthorized();
            }

            try
            {
                await _bookService.HandleDeleteBookAsync(bookId, uid);
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
