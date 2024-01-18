using Domain.Exceptions;
using Domain.Validations;

namespace Domain.Dto
{
    public record BookCreate(string BookName, string[] AuthorNames, string ISBN = "", string BookImageURL = "#") : IValidatable
    {
        public void Validate()
        {
            if (string.IsNullOrEmpty(BookName)) throw new ValidationFailedException("Name cannot be null or empty.");
            if (BookName.Length < 1 || BookName.Length > 200) throw new ValidationFailedException("Length of name cannot be less than 1 character and more than 200 characters.");
            
            if (AuthorNames is null || AuthorNames.Length < 1) throw new ValidationFailedException("Book must have at least 1 author.");
            if (AuthorNames.Length > 25) throw new ValidationFailedException("Book must not have at more than 25 authors.");

            foreach (var author in AuthorNames)
            {
                if (string.IsNullOrEmpty(author)) throw new ValidationFailedException("Author name cannot be empty.");
                if (author.Length > 200) throw new ValidationFailedException("Author name must not have more than 200 characters.");
            }

            if (ISBN is not null && ISBN.Length > 13) throw new ValidationFailedException("ISBN cannot exceed 13 characters.");
            if (BookImageURL is not null && BookImageURL.Length > 1250) throw new ValidationFailedException("Image URL cannot exceed 1250 characters.");
        }
    }
}
