namespace Domain.Dto
{
    public record ModifyBook(string BookId, string BookName, string[] AuthorNames, string ISBN = "", string BookImageURL = "#");
}
