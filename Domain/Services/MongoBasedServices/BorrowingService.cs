using Domain.Dto;

namespace Domain.Services.MongoBasedServices
{
    public class BorrowingService : IBorrowingService
    {
        public BorrowingService()
        {
            
        }

        public Task HandleBorrowingBookAsync(ReturnAndBorrow returnAndBorrow, string userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task HandleReturningBookAsync(ReturnAndBorrow returnAndBorrow, string userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
