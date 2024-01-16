using Domain.BaseEntities;
using Domain.Dto;

namespace Domain.Extensions
{
    internal static class UserExtensions
    {
        internal static UserPublicView AsUserPublicView(this UserBase user)
        {
            return new UserPublicView(user.UserId, user.UserName, user.UserAddress.Street1, user.UserAddress.Street2);
        }

        internal static CurrentUser AsCurrentUser(this UserBase user)
        {
            return new CurrentUser(user.UserId, user.UserName, user.UserAddress.Street1, user.UserAddress.Street2, user.ListedBooks.Select(x => x.BookId).ToArray());
        }
    }
}
