using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public record Pagination(
        [Range(1, short.MaxValue, ErrorMessage = "Page number cannot be less than 1 or greater than 32767")]
        int PageNo = 1,

        [Range(10, 100, ErrorMessage = "Result count per page cannot be less than 10 or greater than 100")]
        int ResultsCount = 10);
}
