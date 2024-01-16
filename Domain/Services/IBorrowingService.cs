using Domain.Dto;
using Domain.Exceptions;

namespace Domain.Services
{
    public interface IBorrowingService
    {
        ///<exception cref="RecordNotFoundException"/>
        ///<exception cref="OperationCanceledException"/>
        Task HandleBorrowingBookAsync(string currentUser, ReturnAndBorrow returnAndBorrow, string userId, CancellationToken cancellationToken = default);

        ///<exception cref="RecordNotFoundException"/>
        ///<exception cref="OperationCanceledException"/>
        Task HandleReturningBookAsync(string currentUser, ReturnAndBorrow returnAndBorrow, string userId, CancellationToken cancellationToken = default);
    }
}
