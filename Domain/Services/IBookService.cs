using Domain.Dto;
using Domain.Exceptions;

namespace Domain.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookMinimalInfo>> GetAllBooksAsync(Pagination pagination, AdvancedBookSearch advancedSearch, CancellationToken cancellationToken = default);

        ///<exception cref="RecordNotFoundException"/>
        ///<exception cref="OperationCanceledException"/>
        Task<BookPublicInfo> GetSpecificBookByIdAsync(string bookId, CancellationToken cancellationToken = default);

        ///<exception cref="RecordNotFoundException"/>
        ///<exception cref="OperationCanceledException"/>
        Task<IEnumerable<BookMinimalInfo>> GetListingsOfCurrentUserAsync(Pagination pagination, string userId, CancellationToken cancellationToken = default);

        ///<exception cref="RecordNotFoundException"/>
        ///<exception cref="OperationCanceledException"/>
        Task<IEnumerable<BookWithBorrower>> GetBorrowedFromCurrentUserAsync(Pagination pagination, string userId, CancellationToken cancellationToken = default);

        ///<exception cref="RecordNotFoundException"/>
        ///<exception cref="OperationCanceledException"/>
        Task<IEnumerable<BookMinimalInfo>> GetBorrowedToCurrentUserAsync(Pagination pagination, string userId, CancellationToken cancellationToken = default);

        ///<exception cref="RecordNotFoundException"/>
        ///<exception cref="ValidationFailedException"/>
        ///<exception cref="OperationCanceledException"/>
        Task HandleModifyBookAsync(ModifyBook modifyBook, CancellationToken cancellationToken = default);

        ///<exception cref="ValidationFailedException"/>
        ///<exception cref="AlreadyExistsException"/>
        ///<exception cref="OperationCanceledException"/>
        Task HandleCreateBookAsync(BookCreate bookCreate, CancellationToken cancellationToken = default);

        ///<exception cref="RecordNotFoundException"/>
        ///<exception cref="OperationCanceledException"/>
        Task HandleDeleteBookAsync(string bookId, CancellationToken cancellationToken = default);
    }
}
