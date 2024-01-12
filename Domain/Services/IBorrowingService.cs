using Domain.Dto;
using Domain.Exceptions;

namespace Domain.Services
{
    public interface IBorrowingService
    {
        ///<exception cref="RecordNotFoundException"/>
        ///<exception cref="OperationCanceledException"/>
        Task HandleBorrowingBookAsync(ReturnAndBorrow returnAndBorrow, CancellationToken cancellationToken = default);

        ///<exception cref="RecordNotFoundException"/>
        ///<exception cref="OperationCanceledException"/>
        Task HandleReturningBookAsync(ReturnAndBorrow returnAndBorrow, CancellationToken cancellationToken = default);
    }
}
