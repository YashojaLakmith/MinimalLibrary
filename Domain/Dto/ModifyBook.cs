using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Domain.Validations;

namespace Domain.Dto
{
    public record ModifyBook(
        [Required(AllowEmptyStrings = false, ErrorMessage = "Book id is required.")]
        [Length(36, 36, ErrorMessage = "Book id must have 36 characters.")]
        string BookId,

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name cannot be null or empty.")]
        [MinLength(1, ErrorMessage = "Length of name cannot be less than 1 character.")]
        [MaxLength(200, ErrorMessage = "Length of name cannot be more than 200 characters.")]
        string BookName,

        [Required(ErrorMessage = "Book must have at least 1 author.")]
        [MinLength(1, ErrorMessage = "Book must have at least 1 author.")]
        [MaxLength(25, ErrorMessage = "Book must not have more than 25 authors")]
        [StringLegthInCollection(1, 200, ErrorMessage = "Author name must be between 1 and 200 characters long.")]
        string[] AuthorNames,

        [MaxLength(13, ErrorMessage = "ISBN cannot be exceed 13 characters.")]
        string ISBN = "",

        [DataType(DataType.Url)]
        string BookImageURL = "#");
}
