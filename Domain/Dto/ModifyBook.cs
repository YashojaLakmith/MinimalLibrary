namespace Domain.Dto
{
    public record ModifyBook(string BookId, string BookName, string ISBN, int Status, string BookImageURL, string[] AuthorNames);
}
