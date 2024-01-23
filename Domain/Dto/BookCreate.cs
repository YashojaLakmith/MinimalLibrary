namespace Domain.Dto
{
    public record BookCreate(string BookName, string[] AuthorNames, string ISBN = "", string BookImageURL = "#");
}
