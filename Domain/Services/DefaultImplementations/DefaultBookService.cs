using Domain.DataAccess;
using Domain.Dto;

namespace Domain.Services.DefaultImplementations
{
    public class DefaultBookService : IBookService
    {
        private readonly IBookDataAccess _bookData;

        public DefaultBookService(IBookDataAccess bookData)
        {
            _bookData = bookData;
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

        public Task HandleCreateBookAsync(BookCreate bookCreate, string userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task HandleDeleteBookAsync(string bookId, string userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task HandleModifyBookAsync(ModifyBook modifyBook, string userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
