namespace Domain.Dto
{
    public record PasswordChange(string UserId, string CurrentPassword, string NewPassword);
}
