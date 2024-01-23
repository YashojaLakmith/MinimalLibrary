using Domain.BaseEntities;
using Domain.DataAccess;
using Domain.Dto;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Validations;

namespace Domain.Services.DefaultImplementations
{
    public class DefaultBookService : IBookService
    {
        private readonly IBookDataAccess _bookData;
        private readonly IUserDataAccess _userData;
        private readonly IInputDataValidations _dataValidations;

        public DefaultBookService(IBookDataAccess bookData, IUserDataAccess userData, IInputDataValidations dataValidations)
        {
            _bookData = bookData;
            _userData = userData;
            _dataValidations = dataValidations;
        }

        public async Task<IEnumerable<BookMinimalInfo>> GetAllBooksAsync(Pagination pagination, AdvancedBookSearch advancedSearch, CancellationToken cancellationToken = default)
        {
            _dataValidations.ValidatePagination(pagination.PageNo, pagination.ResultsCount);

            (int skip, int take) = Utility.GetSkipAndTake(pagination.PageNo, pagination.ResultsCount);
            return (await _bookData.GetAllBooksAsync(advancedSearch.BookName, advancedSearch.AuthorName, skip, take, cancellationToken))
                            .Select(x => x.AsBookMinimalInfo());
        }

        public async Task<IEnumerable<BookWithBorrower>> GetBorrowedFromCurrentUserAsync(Pagination pagination, string userId, CancellationToken cancellationToken = default)
        {
            _dataValidations.ValidatePagination(pagination.PageNo, pagination.ResultsCount);
            _dataValidations.ValidateUserId(userId);

            var user = await _userData.GetUserByIdAsync(userId, cancellationToken) ?? throw new RecordNotFoundException("User");

            (int skip, int take) = Utility.GetSkipAndTake(pagination.PageNo, pagination.ResultsCount);
            return (await _bookData.GetBooksBorrowedFromUserAsync(userId, skip, take))
                            .Select(x => x.AsBookWithBorrower());
        }

        public async Task<IEnumerable<BookMinimalInfo>> GetBorrowedToCurrentUserAsync(Pagination pagination, string userId, CancellationToken cancellationToken = default)
        {
            _dataValidations.ValidatePagination(pagination.PageNo, pagination.ResultsCount);
            _dataValidations.ValidateUserId(userId);

            var user = await _userData.GetUserByIdAsync(userId, cancellationToken) ?? throw new RecordNotFoundException("User");

            (int skip, int take) = Utility.GetSkipAndTake(pagination.PageNo, pagination.ResultsCount);
            return (await _bookData.GetBooksBorrowedToUserAsync(userId, skip, take, cancellationToken))
                            .Select(x => x.AsBookMinimalInfo());
        }

        public async Task<IEnumerable<BookMinimalInfo>> GetListingsOfCurrentUserAsync(Pagination pagination, string userId, CancellationToken cancellationToken = default)
        {
            _dataValidations.ValidatePagination(pagination.PageNo, pagination.ResultsCount);
            _dataValidations.ValidateUserId(userId);

            var user = await _userData.GetUserByIdAsync(userId, cancellationToken) ?? throw new RecordNotFoundException("User");

            (int skip, int take) = Utility.GetSkipAndTake(pagination.PageNo, pagination.ResultsCount);
            return (await _bookData.GetListedBooksOfUserAsync(userId, skip, take, cancellationToken))
                            .Select(x => x.AsBookMinimalInfo());
        }

        public async Task<BookPublicInfo> GetSpecificBookByIdAsync(string bookId, CancellationToken cancellationToken = default)
        {
            _dataValidations.ValidateBookId(bookId);

            var book = await _bookData.GetBookByIdAsync(bookId, cancellationToken) ?? throw new RecordNotFoundException("Book");
            var user = await _userData.GetUserByIdAsync(book.Owner.UserId, cancellationToken) ?? throw new RecordNotFoundException("User");

            book.SetOwner(user);
            return book.AsBookPublicInfo();
        }

        public async Task HandleCreateBookAsync(BookCreate bookCreate, string userId, CancellationToken cancellationToken = default)
        {
            _dataValidations.ValidateBookName(bookCreate.BookName);
            _dataValidations.ValidateAuthorCollection(bookCreate.AuthorNames);
            _dataValidations.ValidateBookISBN(bookCreate.ISBN);
            _dataValidations.ValidateBookImageURL(bookCreate.BookImageURL);
            _dataValidations.ValidateUserId(userId);

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
            _dataValidations.ValidateBookId(bookId);
            _dataValidations.ValidateUserId(userId);

            var book = await _bookData.GetBookByIdAsync(bookId, cancellationToken) ?? throw new RecordNotFoundException("Book");
            if(book.Owner.UserId != userId)
            {
                throw new AuthenticationException();
            }

            await _bookData.DeleteBookAsync(bookId, cancellationToken);
        }

        public async Task HandleModifyBookAsync(ModifyBook modifyBook, string userId, CancellationToken cancellationToken = default)
        {
            _dataValidations.ValidateBookId(modifyBook.BookId);
            _dataValidations.ValidateBookName(modifyBook.BookName);
            _dataValidations.ValidateAuthorCollection(modifyBook.AuthorNames);
            _dataValidations.ValidateBookISBN(modifyBook.ISBN);
            _dataValidations.ValidateBookImageURL(modifyBook.BookImageURL);
            _dataValidations.ValidateUserId(userId);

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
