using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public record UserCreation(
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Id is required.")]
        [MaxLength(100, ErrorMessage = "User Id length must not be more than 100 characters.")]
        string UserId,

        [Required(AllowEmptyStrings = false, ErrorMessage = "User name is required.")]
        [MaxLength(200, ErrorMessage = "User name cannot exceed 200 characters.")]
        string UserName,

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email address is required.")]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])", ErrorMessage = "Must be a valid email address.")]
        string EmailAddress,

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        [RegularExpression(@"^[a-zA-Z0-9!@#$%^&]{6,14}$", ErrorMessage = "The password length must be between 6 and 14 charcters and may contain any alphanumeric characters and !@#$%^&.")]
        string Password,

        [Required(AllowEmptyStrings = false, ErrorMessage = "Address line 1 is required.")]
        [MaxLength(100, ErrorMessage = "Address line 1 cannot exceed 100 characters.")]
        string AddressLine1,

        [MaxLength(100, ErrorMessage = "Address line 2 cannot exceed 100 characters.")]
        string AddressLine2 = "");
}
