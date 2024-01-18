using System.Text.RegularExpressions;

using Domain.Exceptions;
using Domain.Validations;

namespace Domain.Dto
{
    public record PasswordChange(string UserId, string CurrentPassword, string NewPassword) : LoginInformation(UserId, CurrentPassword), IValidatable
    {
        public new void Validate()
        {
            base.Validate();

            if (string.IsNullOrEmpty(NewPassword)) throw new ValidationFailedException("New password is required");
            if (!Regex.IsMatch(NewPassword, @"^[a-zA-Z0-9!@#$%^&]{6,14}$")) throw new ValidationFailedException("The password length must be between 6 and 14 charcters and may contain any alphanumeric characters and !@#$%^&.");
        }
    }
}
