namespace Domain.Dto
{
    public record UserCreation(string UserId, string UserName, string AddressLine1, string AddressLine2, string EmailAddress, string Password);
}
