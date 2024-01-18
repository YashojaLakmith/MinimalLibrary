using System.Text.RegularExpressions;

using Domain.Exceptions;
using Domain.Validations;

namespace Domain.Dto
{
    public record LoginInformation(string UserId, string Password) : IValidatable
    {
        public void Validate()
        {
            if (string.IsNullOrEmpty(UserId)) throw new ValidationFailedException("User Id is required.");
            if (UserId.Length > 100) throw new ValidationFailedException("User Id length must not be more than 100 characters.");

            if (string.IsNullOrEmpty(Password)) throw new ValidationFailedException("Password is required.");
            if (!Regex.IsMatch(Password, @"^[a-zA-Z0-9!@#$%^&]{6,14}$")) throw new ValidationFailedException("The password length must be between 6 and 14 charcters and may contain any alphanumeric characters and !@#$%^&.");
        }
    }
}
