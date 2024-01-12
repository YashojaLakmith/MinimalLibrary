namespace Domain.Dto
{
    public record CurrentUser(string UserId, string UserName, string AddressLine1, string AddressLine2, BookOwnerView[] ListedBooks);
}
