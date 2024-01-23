namespace Domain.Dto
{
    public record UserModification(string UserId, string UserName, string EmailAddress, string Password, string AddressLine1, string AddressLine2 = "");
}
