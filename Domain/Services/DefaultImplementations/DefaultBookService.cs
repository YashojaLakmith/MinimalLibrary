using Domain.BaseEntities;
using Domain.DataAccess;
using Domain.Dto;
using Domain.Exceptions;

namespace Domain.Services.DefaultImplementations
{
    public class DefaultBookService : IBookService
    {
        private readonly IBookDataAccess _bookData;
        private readonly IUserDataAccess _userData;

        public DefaultBookService(IBookDataAccess bookData, IUserDataAccess userData)
        {
            _bookData = bookData;
            _userData = userData;
        }

        public Task<IEnumerable<BookMinimalInfo>> GetAllBooksAsync(Pagination pagination, AdvancedBookSearch advancedSearch, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookWithBorrower>> GetBorrowedFromCurrentUserAsync(Pagination pagination, string userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookMinimalInfo>> GetBorrowedToCurrentUserAsync(Pagination pagination, string userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookMinimalInfo>> GetListingsOfCurrentUserAsync(Pagination pagination, string userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<BookPublicInfo> GetSpecificBookByIdAsync(string bookId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task HandleCreateBookAsync(BookCreate bookCreate, string userId, CancellationToken cancellationToken = default)
        {
            //Needs validation

            var user = await _userData.GetUserByIdAsync(userId, cancellationToken) ?? throw new RecordNotFoundException("User");
            var newBook = Book.CreateBook(Guid.NewGuid().ToString(), bookCreate.BookName)
                                .SetAuthors(bookCreate.AuthorNames)
                                .SetAvailability(BookAvailability.Available)
                                .SetHolder(null)
                                .SetImageURL(bookCreate.BookImageURL)
                                .SetISBN(bookCreate.ISBN)
                                .SetOwner(user);

            await _bookData.CreateBookAsync(newBook, cancellationToken);
        }

        public async Task HandleDeleteBookAsync(string bookId, string userId, CancellationToken cancellationToken = default)
        {
            var book = await _bookData.GetBookByIdAsync(bookId, cancellationToken) ?? throw new RecordNotFoundException("Book");
            if(book.Owner.UserId != userId)
            {
                throw new AuthenticationException();
            }

            await _bookData.DeleteBookAsync(bookId, cancellationToken);
        }

        public async Task HandleModifyBookAsync(ModifyBook modifyBook, string userId, CancellationToken cancellationToken = default)
        {
            // Needs validation

            var book = await _bookData.GetBookByIdAsync(modifyBook.BookId, cancellationToken) ?? throw new RecordNotFoundException("Book");
            if (book.Owner.UserId != userId)
            {
                throw new AuthenticationException();
            }

            book.SetAuthors(modifyBook.AuthorNames)
                .SetImageURL(modifyBook.BookImageURL)
                .SetISBN(modifyBook.ISBN);

            await _bookData.ModifyBookAsync(book, cancellationToken);
        }
    }
}
