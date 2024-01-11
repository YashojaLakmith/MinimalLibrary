using Domain.BaseEntities;

namespace Domain
{
    public class Author : AuthorBase
    {
        private Author(string name) : base(name) { }

        public static Author CreateAuthor(string name)
        {
            throw new NotImplementedException();
        }

        public Author SetName(string name)
        {
            throw new NotImplementedException();
        }

        public Author SetAuthoredBooks(IReadOnlyCollection<BookBase> books)
        {
            throw new NotImplementedException();
        }
    }
}
