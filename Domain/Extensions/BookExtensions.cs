using Domain.BaseEntities;
using Domain.Dto;

namespace Domain.Extensions
{
    internal static class BookExtensions
    {
        internal static BookOwnerView AsBookOwnerView(this BookBase book)
        {
            if(book.CurrentHolder is null)
            {
                return new BookOwnerView(book.BookId, book.BookName, book.ISBN, book.BookImgUrl, book.Authors.ToArray(), null);
            }

            return new BookOwnerView(book.BookId, book.BookName, book.ISBN, book.BookImgUrl, book.Authors.ToArray(), book.CurrentHolder.UserId);
        }
    }
}
