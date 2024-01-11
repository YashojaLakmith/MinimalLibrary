namespace Domain.BaseEntities
{
    public class UserBase : IEquatable<UserBase>
    {
        public string UserId { get; }
        public string UserName { get; protected set; }
        public Address UserAddress { get; protected set; }
        public string EmailAddress { get; protected set; }
        public bool IsActive {  get; protected set; }
        public IReadOnlyCollection<BookBase> ListedBooks { get; protected set; }
        public IReadOnlyCollection<BookBase> BorrowedOut { get; protected set; }
        public IReadOnlyCollection<BookBase> BorrowedIn { get; protected set; }

        protected UserBase(string userId)
        {
            UserId = userId;
            ListedBooks = new List<BookBase>();
            BorrowedIn = new List<BookBase>();
            BorrowedOut = new List<BookBase>();
        }

        public override bool Equals(object? obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public bool Equals(UserBase? other)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(UserBase? left, UserBase? right)
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(UserBase? left, UserBase? right)
        {
            throw new NotImplementedException();
        }
    }
}
