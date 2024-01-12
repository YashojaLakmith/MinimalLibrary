using Domain.Dto;
using Domain.Exceptions;
using Domain.Services;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/borrowings/")]
    public class BookBorrowingController : ControllerBase
    {
        private readonly IBorrowingService _borrowingService;
        private readonly IBookService _bookService;

        public BookBorrowingController(IBorrowingService borrowingService, IBookService bookService)
        {
            _borrowingService = borrowingService;
            _bookService = bookService;
        }

        [HttpGet]
        [Route("in/")]
        public async Task<IActionResult> GetBooksBorrowedByMeAsync([FromQuery] Pagination pagination)
        {
            try
            {
                var books = await _bookService.GetBorrowedToCurrentUserAsync(pagination, "");
                return Ok(books);
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
        [Route("away/")]
        public async Task<IActionResult> GetBooksBorrowedFromMeAsync([FromQuery] Pagination pagination)
        {
            try
            {
                var books = await _bookService.GetBorrowedFromCurrentUserAsync(pagination, "");
                return Ok(books);
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

        [HttpPatch]
        [Route("borrow/")]
        public async Task<IActionResult> BorrowABookAsync([FromQuery] ReturnAndBorrow returnAndBorrow)    // dto-> userid, bookid
        {
            try
            {
                await _borrowingService.HandleBorrowingBookAsync(returnAndBorrow);
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

        [HttpPatch]
        [Route("return/")]
        public async Task<IActionResult> ReturnABookAsync([FromQuery] ReturnAndBorrow returnAndBorrow)   // dto-> userid, bookid
        {
            try
            {
                await _borrowingService.HandleReturningBookAsync(returnAndBorrow);
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
