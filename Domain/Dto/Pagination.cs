using Domain.Exceptions;
using Domain.Validations;

namespace Domain.Dto
{
    public record Pagination(int PageNo = 1, int ResultsCount = 10) : IValidatable
    {
        public void Validate()
        {
            if (PageNo < 1 || PageNo > short.MaxValue) throw new ValidationFailedException($"Page number cannot be less than 1 or greater than {short.MaxValue}.");
            if (ResultsCount < 10 || ResultsCount > 100) throw new ValidationFailedException("Result count per page cannot be less than 10 or greater than 100.");
        }
    }
}
