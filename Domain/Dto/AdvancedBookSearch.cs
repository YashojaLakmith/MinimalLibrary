using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public record AdvancedBookSearch(
        [MaxLength(200, ErrorMessage = "Book name cannot exceed 150 characters.")]
        string? BookName = null,

        [MaxLength(200, ErrorMessage = "Author name cannot exceed 150 characters.")]
        string? AuthorName = null);
}
