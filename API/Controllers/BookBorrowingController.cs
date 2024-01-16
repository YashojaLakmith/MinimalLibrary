using Domain.Dto;
using Domain.Exceptions;
using Domain.Services;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/borrowings/")]
    public class BookBorrowingController : BaseApiController
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
            var uid = GetCuurentUserId();
            if (uid is null)
            {
                return Unauthorized();
            }

            try
            {
                var books = await _bookService.GetBorrowedToCurrentUserAsync(pagination, uid);
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
            var uid = GetCuurentUserId();
            if (uid is null)
            {
                return Unauthorized();
            }

            try
            {
                var books = await _bookService.GetBorrowedFromCurrentUserAsync(pagination, uid);
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
            var uid = GetCuurentUserId();
            if(uid is null)
            {
                return Unauthorized();
            }

            try
            {
                await _borrowingService.HandleBorrowingBookAsync(returnAndBorrow, uid);
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
        [Route("return/")]
        public async Task<IActionResult> ReturnABookAsync([FromQuery] ReturnAndBorrow returnAndBorrow)   // dto-> userid, bookid
        {
            var uid = GetCuurentUserId();
            if (uid is null)
            {
                return Unauthorized();
            }

            try
            {
                await _borrowingService.HandleReturningBookAsync(returnAndBorrow, uid);
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
