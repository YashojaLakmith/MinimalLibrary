using Domain.BaseEntities;

namespace Domain
{
    /// <summary>
    /// Represents a Book.
    /// </summary>
    public class Book : BookBase
    {
        private Book(string id, string name) : base(id, name) { }

        public static Book CreateBook(string id, string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            
            return new Book(id, name);
        }

        public Book SetOwner(UserBase owner)
        {
            Owner = owner;
            return this;
        }

        public Book SetISBN(string isbn)
        {
            ArgumentNullException.ThrowIfNull(isbn);
            return this;
        }

        public Book SetHolder(UserBase? holder)
        {
            CurrentHolder = holder;
            return this;
        }

        public Book SetImageURL(string imageURL)
        {
            if (string.IsNullOrEmpty(imageURL))
            {
                BookImgUrl = "#";
            }
            else
            {
                BookImgUrl = imageURL;
            }

            return this;
        }

        public Book SetAvailability(BookAvailability availability)
        {
            BookAvailability = availability;
            return this;
        }

        public Book SetAuthors(IReadOnlyCollection<string> authors)
        {
            Authors = authors;
            return this;
        }
    }
}
