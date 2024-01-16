using System.Text.RegularExpressions;

using Domain.Dto;
using Domain.Exceptions;

namespace Domain.Validations
{
    internal static partial class BasicValidation
    {
        internal static void ValidatePagination(Pagination pagination)
        {
            if(pagination.PageNo < 1)
            {
                throw new ValidationFailedException(nameof(pagination.PageNo));
            }

            if(pagination.ResultsCount < 10)
            {
                throw new ValidationFailedException(nameof(pagination.ResultsCount));
            }
        }

        internal static bool ValidatePassword(string pw)
        {
            return PasswordRegex().IsMatch(pw);
        }

        [GeneratedRegex("^[a-zA-Z0-9!@#$%^&]{6,14}$")]
        private static partial Regex PasswordRegex();
    }
}
