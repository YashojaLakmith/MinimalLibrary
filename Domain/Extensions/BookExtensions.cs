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

        internal static BookMinimalInfo AsBookMinimalInfo(this BookBase book)
        {
            return new BookMinimalInfo(book.BookId, book.BookName, book.Owner.UserName, book.CurrentHolder is null);
        }

        internal static BookPublicInfo AsBookPublicInfo(this BookBase book)
        {
            return new BookPublicInfo(book.BookId, book.BookName, book.ISBN, book.Authors.ToArray(), book.Owner.AsUserPublicView(), book.CurrentHolder is null);
        }

        internal static BookWithBorrower AsBookWithBorrower(this BookBase book)
        {
            return new BookWithBorrower(book.BookId, book.BookName, book.CurrentHolder?.UserId);
        }
    }
}
