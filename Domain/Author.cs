using Domain.BaseEntities;

namespace Domain
{
    public class Author : AuthorBase
    {
        private Author(string name) : base(name) { }

        public static Author CreateAuthor(string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            return new Author(name);
        }

        public Author SetName(string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            AuthorName = name;
            return this;
        }

        public Author SetAuthoredBooks(IReadOnlyCollection<BookBase> books)
        {
            AuthoredBooks = books;
            return this;
        }
    }
}
