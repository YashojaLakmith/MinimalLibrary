namespace Domain.BaseEntities
{
    public abstract class AuthorBase : IEquatable<AuthorBase>
    {
        public string AuthorName { get; protected set; }
        public IReadOnlyCollection<BookBase> AuthoredBooks { get; protected set; }

        protected AuthorBase(string name)
        {
            AuthorName = name;
        }

        public override bool Equals(object? obj)
        {
            throw new NotImplementedException();
        }

        public bool Equals(AuthorBase? other)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(AuthorBase? lhs, AuthorBase? rhs)
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(AuthorBase? lhs, AuthorBase? rhs)
        {
            throw new NotImplementedException();
        }
    }
}
