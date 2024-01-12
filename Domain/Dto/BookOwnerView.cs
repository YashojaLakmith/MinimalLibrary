namespace Domain.Dto
{
    public record BookOwnerView(string BookId, string BookName, string ISBN, string ImageURL, string[] Authors, UserPublicView CurrentHolder);
}
