using Domain.BaseEntities;

namespace Domain
{
    /// <summary>
    /// Represents a Book.
    /// </summary>
    public class Book : BookBase
    {
        private Book(string id, string name, UserBase user) : base(id, name, user) { }

        public static Book CreateBook(string id, string name, UserBase user)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            
            return new Book(id, name, user);
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

        public Book SetAuthors(IReadOnlyCollection<AuthorBase> authors)
        {
            Authors = authors;
            return this;
        }
    }
}
