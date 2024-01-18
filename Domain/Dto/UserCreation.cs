using System.Text.RegularExpressions;

using Domain.Exceptions;
using Domain.Validations;

namespace Domain.Dto
{
    public record UserCreation(string UserId, string UserName, string EmailAddress, string Password, string AddressLine1, string AddressLine2 = "") : IValidatable
    {
        public void Validate()
        {
            string emailPattern = @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])";
            string pwPattern = @"^[a-zA-Z0-9!@#$%^&]{6,14}$";

            if (string.IsNullOrEmpty(UserId)) throw new ValidationFailedException("User Id is required.");
            if (UserId.Length > 100) throw new ValidationFailedException("User Id length must not be more than 100 characters.");

            if (string.IsNullOrEmpty(UserName)) throw new ValidationFailedException("UserName is required");
            if (UserName.Length > 200) throw new ValidationFailedException("User name cannot exceed 200 characters.");

            if (string.IsNullOrEmpty(EmailAddress)) throw new ValidationFailedException("Email is required");
            if (!Regex.IsMatch(EmailAddress, emailPattern)) throw new ValidationFailedException("Email must be a valid one.");

            if (string.IsNullOrEmpty(Password)) throw new ValidationFailedException("Password is required");
            if (!Regex.IsMatch(Password, pwPattern)) throw new ValidationFailedException("The password length must be between 6 and 14 charcters and may contain any alphanumeric characters and !@#$%^&.");

            if (string.IsNullOrEmpty(AddressLine1)) throw new ValidationFailedException("AddressLine 1 is required");
            if (AddressLine1.Length > 100) throw new ValidationFailedException("AddressLine 1 should not have more than 100 character");

            if (AddressLine1 is not null && AddressLine1.Length > 100) throw new ValidationFailedException("AddressLine 1 should not have more than 100 character");
        }
    }
}
