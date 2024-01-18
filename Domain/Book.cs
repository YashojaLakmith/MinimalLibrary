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

        public override Book SetOwner(UserBase owner)
        {
            Owner = owner;
            return this;
        }

        public override Book SetISBN(string isbn)
        {
            ArgumentNullException.ThrowIfNull(isbn);
            return this;
        }

        public override Book SetHolder(UserBase? holder)
        {
            CurrentHolder = holder;
            return this;
        }

        public override Book SetImageURL(string imageURL)
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

        public override Book SetAvailability(BookAvailability availability)
        {
            BookAvailability = availability;
            return this;
        }

        public override Book SetAuthors(IEnumerable<string> authors)
        {
            Authors = authors;
            return this;
        }

        public override bool IsValidModel()
        {
            if (string.IsNullOrEmpty(BookId)) return false;            
            if(string.IsNullOrEmpty(BookName)) return false;
            if (Owner is null) return false;
            if (!Authors.Any()) return false;

            return true;
        }
    }
}
