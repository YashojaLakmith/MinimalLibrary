namespace Domain.Dto
{
    public record AdvancedUserSearch(int PageNo, int ResultCount, string? UserName, string? BookName) : Pagination(PageNo, ResultCount);
}
