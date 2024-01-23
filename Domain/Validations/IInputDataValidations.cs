namespace Domain.Validations
{
    /// <summary>
    /// Defines methods for validating various input fields.
    /// </summary>
    public interface IInputDataValidations
    {
        void ValidateUserId(string? userId);
        void ValidateUserName(string? userName);
        void ValidatePassword(string? password);
        void ValidateEmail(string? email);
        void ValidatePagination(int? pageNo, int? recordCount);
        void ValidateAddressLines(string? addressLine1, string? addressLine2);

        void ValidateBookId(string? bookId);
        void ValidateBookName(string? bookName);
        void ValidateAuthorName(string? authorName);
        void ValidateAuthorCollection(IEnumerable<string?>? authors);
        void ValidateBookISBN(string? bookisbn);
        void ValidateBookImageURL(string? bookImageURL);
    }
}
