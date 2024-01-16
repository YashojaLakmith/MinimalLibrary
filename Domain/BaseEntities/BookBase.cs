namespace Domain.BaseEntities
{
    /// <summary>
    /// Base class for Books
    /// </summary>
    public abstract class BookBase : IEquatable<BookBase>
    {
        public string BookId { get; }
        public string BookName { get; }
        public string ISBN { get; protected set; }
        public UserBase Owner { get; protected set; }
        public UserBase? CurrentHolder { get; protected set; }
        public string BookImgUrl { get; protected set; }
        public IReadOnlyCollection<string> Authors { get; protected set; }
        public BookAvailability BookAvailability { get; protected set; }

        protected BookBase(string id, string bookName, UserBase owner)
        {
            BookId = id;
            BookName = bookName;
            Owner = owner;
            Authors = new List<string>();
            BookAvailability = BookAvailability.Available;
        }

        public override int GetHashCode()
        {
            return BookId.GetHashCode();
        }

        public bool Equals(BookBase? other)
        {
            if(other is null)
            {
                return false;
            }

            return BookId == other.BookId;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as BookBase);
        }

        public static bool operator ==(BookBase? left, BookBase? right)
        {
            if(left is null || right is null)
            {
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(BookBase? left, BookBase? right)
        {
            return !(left == right);
        }
    }
}
