using System.Security.Cryptography;
using System.Text.RegularExpressions;

using Domain.BaseEntities;

namespace Domain
{
    /// <summary>
    /// Represents a User account and associated password credentials.
    /// </summary>
    public class User : UserBase
    {
        public byte[] PasswordHash { get; private set; }
        public byte[] Salt { get; private set; }

        private User(string userId): base(userId) { }

        public static User CreateUser(string id)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));

            return new User(id);
        }

        public User SetUserName(string userName)
        {
            ArgumentException.ThrowIfNullOrEmpty(userName, nameof(userName));

            UserName = userName;
            return this;
        }

        public User SetAddress(Address addr)
        {
            UserAddress = addr;
            return this;
        }

        public User SetEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if(!Regex.IsMatch(email, pattern))
            {
                throw new ArgumentException(nameof(email));
            }

            EmailAddress = email;
            return this;
        }

        public User SetActivationStatus(bool status)
        {
            IsActive = status;
            return this;
        }

        public User SetListedBooks(IReadOnlyCollection<BookBase> books)
        {
            ListedBooks = books;
            return this;
        }

        public User SetBorrowedInBooks(IReadOnlyCollection<BookBase> books)
        {
            BorrowedIn = books;
            return this;
        }

        public User SetBorrowedAwayBooks(IReadOnlyCollection<BookBase> books)
        {
            BorrowedOut = books;
            return this;
        }

        public bool VerifyPassword(byte[] pw)
        {
            ArgumentNullException.ThrowIfNull(pw, nameof(pw));

            var hash = Cryptographic.GeneratePassword(pw, Salt, 32);

            return hash.Length == PasswordHash.Length && hash.SequenceEqual(PasswordHash);
        }

        public void SetPassword(byte[] pw)
        {
            ArgumentNullException.ThrowIfNull(pw);

            var salt = RandomNumberGenerator.GetBytes(32);
            PasswordHash = Cryptographic.GeneratePassword(pw, salt, 32);
            Salt = salt;
        }

        public User SetPasswordAndSalt(byte[] pw, byte[] salt)
        {
            ArgumentNullException.ThrowIfNull(pw);
            ArgumentNullException.ThrowIfNull(salt);

            PasswordHash = pw;
            Salt = salt;
            return this;
        }
    }
}
