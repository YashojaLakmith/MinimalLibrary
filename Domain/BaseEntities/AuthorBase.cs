namespace Domain.BaseEntities
{
    /// <summary>
    /// Base class for Authors
    /// </summary>
    public abstract class AuthorBase : IEquatable<AuthorBase>
    {
        public string AuthorName { get; protected set; }
        public IReadOnlyCollection<BookBase> AuthoredBooks { get; protected set; }

        protected AuthorBase(string name)
        {
            AuthorName = name;
            AuthoredBooks = new List<BookBase>();
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as AuthorBase);
        }

        public bool Equals(AuthorBase? other)
        {
            if(other is null)
            {
                return false;
            }

            return AuthorName == other.AuthorName;
        }

        public override int GetHashCode()
        {
            return AuthorName.GetHashCode();
        }

        public static bool operator ==(AuthorBase? lhs, AuthorBase? rhs)
        {
            if(lhs is null)
            {
                return false;
            }

            return lhs.Equals(rhs);
        }

        public static bool operator !=(AuthorBase? lhs, AuthorBase? rhs)
        {
            return !(lhs == rhs);
        }
    }
}
