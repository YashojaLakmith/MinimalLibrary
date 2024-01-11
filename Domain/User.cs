using Domain.BaseEntities;

namespace Domain
{
    public class User : UserBase
    {
        public byte[] PasswordHash { get; private set; }
        public byte[] Salt { get; private set; }

        private User(string userId): base(userId) { }

        public static User CreateUser(string id)
        {
            throw new NotImplementedException();
        }

        public User SetUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public User SetAddress(Address addr)
        {
            throw new NotImplementedException();
        }

        public User SetActivationStatus(bool status)
        {
            throw new NotImplementedException();
        }

        public User SetListedBooks(IReadOnlyCollection<BookBase> books)
        {
            throw new NotImplementedException();
        }

        public User SetBorrowedInBooks(IReadOnlyCollection<BookBase> books)
        {
            throw new NotImplementedException();
        }

        public User SetBorrowedAwayBooks(IReadOnlyCollection<BookBase> books)
        {
            throw new NotImplementedException();
        }

        public void SetPassword(byte[] pw)
        {
            throw new NotImplementedException();
        }

        public User SetPasswordAndSalt(byte[] pw, byte[] bytes)
        {
            throw new NotImplementedException();
        }
    }
}
