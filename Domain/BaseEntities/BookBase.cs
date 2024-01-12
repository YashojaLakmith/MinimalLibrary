namespace Domain.BaseEntities
{
    public abstract class BookBase
    {
        public string BookId { get; }
        public string BookName { get; }
        public string ISBN { get; protected set; }
        public UserBase Owner { get; protected set; }
        public UserBase? CurrentHolder { get; protected set; }
        public string BookImgUrl { get; protected set; }
        public IReadOnlyCollection<AuthorBase> Authors { get; protected set; }
        public BookAvailability BookAvailability { get; protected set; }

        protected BookBase(string bookName, UserBase owner)
        {
            BookName = bookName;
            Owner = owner;
            Authors = new List<AuthorBase>();
            BookAvailability = BookAvailability.Available;
        }
    }
}
