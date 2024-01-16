namespace Domain.Dto
{
    public record BookPublicInfo(string BookId, string BookName, string ISBN, string[] Authors, string OwnerId, int Availability);
}
