using Domain.DataAccess;
using Domain.Dto;
using Domain.Exceptions;

namespace Domain.Services.DefaultImplementations
{
    public class DefaultBorrowingService : IBorrowingService
    {
        private readonly IUserDataAccess _userData;
        private readonly IBookDataAccess _bookData;

        public DefaultBorrowingService(IUserDataAccess userData, IBookDataAccess bookData)
        {
            _userData = userData;
            _bookData = bookData;
        }

        public async Task HandleBorrowingBookAsync(ReturnAndBorrow returnAndBorrow, string userId, CancellationToken cancellationToken = default)
        {
            returnAndBorrow.Validate();

            if(userId == returnAndBorrow.UserId)
            {
                throw new ValidationFailedException("Owner and borrower cannot be the same");
            }

            var book = await _bookData.GetBookByIdAsync(returnAndBorrow.BookId, cancellationToken) ?? throw new RecordNotFoundException("Book");
            var user = await _userData.GetUserByIdAsync(returnAndBorrow.UserId, cancellationToken) ?? throw new RecordNotFoundException("User");
            
            if(book.Owner.UserId != userId)
            {
                throw new ValidationFailedException("Owner");
            }

            if(book.CurrentHolder is not null)
            {
                throw new ValidationFailedException("Holder");
            }

            book.SetHolder(user);
            await _bookData.ModifyBookAsync(book, cancellationToken);
        }

        public async Task HandleReturningBookAsync(ReturnAndBorrow returnAndBorrow, string userId, CancellationToken cancellationToken = default)
        {
            returnAndBorrow.Validate();

            if (userId == returnAndBorrow.UserId)
            {
                throw new ValidationFailedException("Owner and borrower cannot be the same");
            }

            var book = await _bookData.GetBookByIdAsync(returnAndBorrow.BookId, cancellationToken) ?? throw new RecordNotFoundException("Book");
            var user = await _userData.GetUserByIdAsync(returnAndBorrow.UserId, cancellationToken) ?? throw new RecordNotFoundException("User");

            if(book.Owner.UserId != userId)
            {
                throw new ValidationFailedException("Owner");
            }

            if(book.CurrentHolder is null)
            {
                throw new ValidationFailedException("Holder");
            }

            book.SetHolder(user);
            await _bookData.ModifyBookAsync(book, cancellationToken);
        }
    }
}
