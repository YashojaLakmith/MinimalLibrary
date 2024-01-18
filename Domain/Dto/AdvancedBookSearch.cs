using Domain.Exceptions;
using Domain.Validations;

namespace Domain.Dto
{
    public record AdvancedBookSearch(string? BookName = null, string? AuthorName = null) : IValidatable
    {
        public void Validate()
        {
            if (BookName is not null && BookName.Length > 200) throw new ValidationFailedException("Book name cannot exceed 200 characters.");
            if (AuthorName is not null && AuthorName.Length > 200) throw new ValidationFailedException("Author name cannot exceed 200 characters.");
        }
    }
}
