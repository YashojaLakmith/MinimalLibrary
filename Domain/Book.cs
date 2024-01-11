using Domain.BaseEntities;

namespace Domain
{
    public class Book : BookBase
    {
        private Book(string name, UserBase user) : base(name, user) { }

        public static Book CreateBook(string name, UserBase user)
        {
            throw new NotImplementedException();
        }

        public Book SetISBN(string isbn)
        {
            throw new NotImplementedException();
        }

        public Book SetHolder(UserBase holder)
        {
            throw new NotImplementedException();
        }

        public Book SetImageURL(string imageURL)
        {
            throw new NotImplementedException();
        }

        public Book SetAvailability(BookAvailability availability)
        {
            throw new NotImplementedException();
        }

        public Book SetAuthors(IReadOnlyCollection<AuthorBase> authors)
        {
            throw new NotImplementedException();
        }
    }
}
