namespace Domain.Dto
{
    public record BookWithBorrower(string BookId, string BookName, string? BorrowerId);
}
