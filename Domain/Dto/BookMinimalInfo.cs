namespace Domain.Dto
{
    public record BookMinimalInfo(string BookId, string BookName, string OwnerName, bool IsAvailable);
}
