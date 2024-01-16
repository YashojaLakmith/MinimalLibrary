namespace Domain.Dto
{
    public record AdvancedUserSearch(int PageNo, int ResultCount, string? UserName) : Pagination(PageNo, ResultCount);
}
