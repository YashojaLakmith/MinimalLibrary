using Domain.Dto;

namespace Domain.Services.MongoBasedServices
{
    public class BookService : IBookService
    {
        public BookService()
        {
            
        }

        public Task<IEnumerable<BookMinimalInfo>> GetAllBooksAsync(Pagination pagination, AdvancedBookSearch advancedSearch, CancellationToken cancellationToken = default)
        {
            /*  pagination validation
                deconstruct search fields
                repo.getAll(filters, skip, take)
                cast
                return
            */
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookWithBorrower>> GetBorrowedFromCurrentUserAsync(Pagination pagination, string userId, CancellationToken cancellationToken = default)
        {
            /*
                pagination validation
                check existance of user
                repo.getBorrowedFromCurrentUser(uid)
            */
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookMinimalInfo>> GetBorrowedToCurrentUserAsync(Pagination pagination, string userId, CancellationToken cancellationToken = default)
        {
            /*
                pagination validation
                check existance of user
                repo.getBorrowedToCurrentUser(uid)
            */

            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookMinimalInfo>> GetListingsOfCurrentUserAsync(Pagination pagination, string userId, CancellationToken cancellationToken = default)
        {
            /*
                pagination validation
                check existance of user
                repo.getListingsOfCurrentUser(uid)
            */

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
