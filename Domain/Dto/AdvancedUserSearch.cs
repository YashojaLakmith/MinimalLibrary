namespace Domain.Dto
{
    public record AdvancedUserSearch(int PageNo = 1, int ResultCount = 100, string? UserName = null) : Pagination(PageNo, ResultCount);
}
