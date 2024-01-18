using Domain.Dto;
using Domain.Exceptions;
using Domain.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/v1/book/")]
    public class BookController : BaseApiController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync([FromQuery] Pagination pagination, [FromBody] AdvancedBookSearch advancedSearch)
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync(pagination, advancedSearch);
                return Ok(books);
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
        [Route("my/")]
        public async Task<IActionResult> GetMyListedBooks([FromQuery] Pagination pagination)
        {
            var id = GetCurrentUserId();

            if(id is null)
            {
                return Unauthorized("There is not active session.");
            }

            try
            {
                var books = await _bookService.GetListingsOfCurrentUserAsync(pagination, id);
                return Ok(books);
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

        [HttpGet]
        [Route("{bookId}")]
        public async Task<IActionResult> GetBookByIdAsync([FromRoute] string bookId)
        {
            try
            {
                var book = await _bookService.GetSpecificBookByIdAsync(bookId);
                return Ok(book);
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

        [HttpPost]
        public async Task<IActionResult> CreateABookAsync([FromBody] BookCreate bookCreate)
        {
            var uid = GetCurrentUserId();
            if (uid is null)
            {
                return Unauthorized("There is not active session.");
            }

            try
            {
                await _bookService.HandleCreateBookAsync(bookCreate, uid);
                return Created();
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
        public async Task<IActionResult> ModifyBookAsync([FromBody] ModifyBook modifyBook)
        {
            var uid = GetCurrentUserId();
            if (uid is null)
            {
                return Unauthorized("There is not active session.");
            }

            try
            {
                await _bookService.HandleModifyBookAsync(modifyBook, uid);
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
        [Route("{bookId}")]
        public async Task<IActionResult> DeleteBookAsync([FromRoute] string bookId)
        {
            var uid = GetCurrentUserId();
            if (uid is null)
            {
                return Unauthorized("There is not active session.");
            }

            try
            {
                await _bookService.HandleDeleteBookAsync(bookId, uid);
                return Ok();
            }
            catch(AuthenticationException ex)
            {
                return Unauthorized(ex.Message);
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
