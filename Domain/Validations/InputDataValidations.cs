using System.Text.RegularExpressions;

using Domain.Exceptions;

namespace Domain.Validations
{
    public class InputDataValidations : IInputDataValidations
    {
        public void ValidateAddressLines(string? addressLine1, string? addressLine2)
        {
            if (string.IsNullOrEmpty(addressLine1)) throw new ValidationFailedException("AddressLine 1 is required");
            if (addressLine1.Length > 100) throw new ValidationFailedException("AddressLine 1 should not have more than 100 character");

            if (addressLine2 is null) throw new ValidationFailedException("AddressLine 2 is cannot be null");
            if (addressLine2.Length > 100) throw new ValidationFailedException("AddressLine 2 should not have more than 100 character");
        }

        public void ValidateAuthorCollection(IEnumerable<string?>? authors)
        {
            if (authors is null || authors.Count() < 1) throw new ValidationFailedException("A book must have at least 1 author.");

            foreach (var author in authors)
            {
                ValidateAuthorName(author);
            }
        }

        public void ValidateAuthorName(string? authorName)
        {
            if (string.IsNullOrEmpty(authorName)) throw new ValidationFailedException("Author name cannot be empty.");
            if (authorName.Length > 200) throw new ValidationFailedException("Author name must not have more than 200 characters.");
        }

        public void ValidateBookId(string? bookId)
        {
            if (string.IsNullOrEmpty(bookId)) throw new ValidationFailedException("Book Id cannot be null or empty.");
            if (bookId.Length != 36) throw new ValidationFailedException("Book Id must have 36 characters.");
        }

        public void ValidateBookImageURL(string? bookImageURL)
        {
            if (bookImageURL is null) throw new ValidationFailedException("Book image URL cannot be null.");
            if (bookImageURL.Length > 1250) throw new ValidationFailedException("Book image URL cannot exceed 1250 characters.");
        }

        public void ValidateBookISBN(string? bookisbn)
        {
            if (bookisbn is null) throw new ValidationFailedException("ISBN cannot be null");
            if (bookisbn.Length > 13) throw new ValidationFailedException("ISBN cannot exceed 13 characters.");
        }

        public void ValidateBookName(string? bookName)
        {
            if (string.IsNullOrEmpty(bookName)) throw new ValidationFailedException("Book name cannot be null or empty.");
            if (bookName.Length < 1 || bookName.Length > 200) throw new ValidationFailedException("Length of book name cannot be less than 1 character and more than 200 characters.");
        }

        public void ValidateEmail(string? email)
        {
            string emailPattern = @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])";
            
            if (string.IsNullOrEmpty(email)) throw new ValidationFailedException("Email is required");
            if (!Regex.IsMatch(email, emailPattern)) throw new ValidationFailedException("Email must be a valid one.");
        }

        public void ValidatePagination(int? pageNo, int? recordCount)
        {
            if (pageNo is null) throw new ValidationFailedException("Page number is required.");
            if (recordCount is null) throw new ValidationFailedException("Record count per page is required");

            if (pageNo < 1 || pageNo > short.MaxValue) throw new ValidationFailedException($"Page number cannot be less than 1 or exceed {short.MaxValue}.");
            if (recordCount < 10 || recordCount > 100) throw new ValidationFailedException("Record count per page cannot be less than 10 or exceed 100.");
        }

        public void ValidatePassword(string? password)
        {
            string passwordPattern = @"^[a-zA-Z0-9!@#$%^&]{6,14}$";

            if (string.IsNullOrEmpty(password)) throw new ValidationFailedException("Password is required");
            if (!Regex.IsMatch(password, passwordPattern)) throw new ValidationFailedException("The password length must be between 6 and 14 charcters and may contain any alphanumeric characters and !@#$%^&.");
        }

        public void ValidateUserId(string? userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ValidationFailedException("User Id is required.");
            if (userId.Length > 100) throw new ValidationFailedException("User Id length must not be more than 100 characters.");
        }

        public void ValidateUserName(string? userName)
        {
            if (string.IsNullOrEmpty(userName)) throw new ValidationFailedException("UserName is required");
            if (userName.Length > 200) throw new ValidationFailedException("User name cannot exceed 200 characters.");
        }
    }
}
