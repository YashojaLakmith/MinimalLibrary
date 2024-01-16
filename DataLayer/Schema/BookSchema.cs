using Domain;
using Domain.BaseEntities;

using MongoDB.Bson.Serialization.Attributes;

namespace DataLayer.Schema
{
    [BsonIgnoreExtraElements]
    public class BookSchema
    {
        public string BookId { get; set; }
        public string BookName { get; set; }
        public string ISBN { get; set; }
        public string OwnerId { get; set; }
        public string? CurrentHolderId { get; set; }
        public string BookImgUrl { get; set; }
        public string[] Authors { get; set; }
        public BookAvailability BookAvailability { get; set; }

        public Book AsEntity()
        {
            var book = Book.CreateBook(BookId, BookName)
                            .SetISBN(ISBN)
                            .SetImageURL(BookImgUrl)
                            .SetAvailability(BookAvailability)
                            .SetOwner(User.CreateUser(OwnerId));
            if(CurrentHolderId is null)
            {
                return book.SetHolder(null);
            }

            return book.SetHolder(User.CreateUser(CurrentHolderId));
        }

        public static BookSchema AsSchema(Book book)
        {
            return new BookSchema()
            {
                BookId = book.BookId,
                BookName = book.BookName,
                ISBN = book.ISBN,
                OwnerId = book.Owner.UserId,
                CurrentHolderId = book.CurrentHolder?.UserId,
                BookImgUrl = book.BookImgUrl,
                Authors = book.Authors.ToArray(),
                BookAvailability = book.BookAvailability
            };
        }
    }
}
