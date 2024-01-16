using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public record ReturnAndBorrow(
        [Required(AllowEmptyStrings = false, ErrorMessage = "Book id is required.")]
        [Length(36, 36, ErrorMessage = "Book id must have 36 characters.")]
        string BookId,

        [Required(AllowEmptyStrings = false, ErrorMessage = "User Id is required.")]
        [MaxLength(100, ErrorMessage = "User Id length must not be more than 100 characters.")]
        string UserId);
}
