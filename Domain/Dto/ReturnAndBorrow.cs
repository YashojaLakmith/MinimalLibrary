using Domain.Exceptions;
using Domain.Validations;

namespace Domain.Dto
{
    public record ReturnAndBorrow(string BookId, string UserId) : IValidatable
    {
        public void Validate()
        {
            if (string.IsNullOrEmpty(BookId)) throw new ValidationFailedException("Book Id is required.");
            if (BookId.Length != 36) throw new ValidationFailedException("Id must have 36 characters.");

            if (string.IsNullOrEmpty(UserId)) throw new ValidationFailedException("User Id is required.");
            if (UserId.Length > 100) throw new ValidationFailedException("User Id length must not be more than 100 characters.");
        }
    }
}
