using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public record LoginInformation(
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Id is required.")]
        [MaxLength(100, ErrorMessage = "User Id length must not be more than 100 characters.")]
        string UserId,

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        [RegularExpression(@"^[a-zA-Z0-9!@#$%^&]{6,14}$", ErrorMessage = "The password length must be between 6 and 14 charcters and may contain any alphanumeric characters and !@#$%^&.")]
        string Password);
}
