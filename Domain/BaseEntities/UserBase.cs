﻿using System.Text.RegularExpressions;

namespace Domain.BaseEntities
{
    /// <summary>
    /// Base class for Users
    /// </summary>
    public abstract class UserBase : IEquatable<UserBase>
    {
        public string UserId { get; }
        public string UserName { get; protected set; }
        public Address UserAddress { get; protected set; }
        public string EmailAddress { get; protected set; }
        public bool IsActive {  get; protected set; }
        public IEnumerable<BookBase> ListedBooks { get; protected set; }
        public IEnumerable<BookBase> BorrowedOut { get; protected set; }
        public IEnumerable<BookBase> BorrowedIn { get; protected set; }

        protected UserBase(string userId)
        {
            UserId = userId;
            ListedBooks = new List<BookBase>();
            BorrowedIn = new List<BookBase>();
            BorrowedOut = new List<BookBase>();
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as UserBase);
        }

        public override int GetHashCode()
        {
            return UserId.GetHashCode();
        }

        public bool Equals(UserBase? other)
        {
            if(other is null)
            {
                return false;
            }
            return other.UserId == UserId;
        }

        public static bool operator ==(UserBase? left, UserBase? right)
        {
            if(left is null || right is null)
            {
                return false;
            }
            return left.Equals(right);
        }

        public static bool operator !=(UserBase? left, UserBase? right)
        {
            return !(left == right);
        }

        public abstract UserBase SetUserName(string userName);

        public abstract UserBase SetAddress(Address addr);

        public abstract UserBase SetEmail(string email);

        public abstract UserBase SetActivationStatus(bool status);

        public abstract UserBase SetListedBooks(IEnumerable<BookBase> books);

        public abstract UserBase SetBorrowedInBooks(IEnumerable<BookBase> books);

        public abstract UserBase SetBorrowedAwayBooks(IEnumerable<BookBase> books);

        public abstract bool IsValidModel();
    }
}
