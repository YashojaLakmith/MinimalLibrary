using Domain;
using Domain.BaseEntities;

using FluentAssertions;

using Xunit;

namespace DomainTests
{
    public class UserTests
    {
        [Fact]
        public void CreateUser_WithNonNullEmptyOrWhitespaceId_ShouldBeOfTypeUser()
        {
            var user = User.CreateUser(Guid.NewGuid().ToString());

            user.Should().BeOfType<User>();
        }

        [Fact]
        public void CreateUser_WithNullEmptyOrWhitespaceId_ShouldBeNull()
        {
            var user = User.CreateUser(string.Empty);

            user.Should().Be(null);
        }

        [Fact]
        public void CreateUser_WithNonNullEmptyOrWhiteSpaceId_ShouldHaveTheId()
        {
            var id = Guid.NewGuid().ToString();
            var user = User.CreateUser(id);

            id.Should().BeEquivalentTo(user.UserId);
        }

        [Fact]
        public void SetUserName_WithEmptyString_ShouldThrowArgumentException()
        {
            var user = User.CreateUser(Guid.NewGuid().ToString());
            var act = () => user.SetUserName(string.Empty);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void SetUserName_WithNonEmptyString_ShouldBeSameAsUserNameProperty()
        {
            var user = User.CreateUser(Guid.NewGuid().ToString());
            var name = Guid.NewGuid().ToString();

            user = user.SetUserName(name);

            name.Should().BeEquivalentTo(user.UserName);
        }

        [Fact]
        public void SetUserAddress_WithAddressObj_ShouldBeSameAsUserAddressProperty()
        {
            var user = User.CreateUser(Guid.NewGuid().ToString());
            var address = Address.CreateAddress(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            user = user.SetAddress(address);
            var result = address.Equals(user.UserAddress);

            result.Should().BeTrue();
        }

        [Fact]
        public void SetActivationStatus_WithStatus_ShouldBeSameAsUserActivationStatus()
        {
            var user = User.CreateUser(Guid.NewGuid().ToString());
            bool status = true;

            user = user.SetActivationStatus(status);

            user.IsActive.Should().Be(status);
        }

        [Fact]
        public void SetListedBooks_WithCollection_ShouldBeSameAsUserListedBooks()
        {
            var user = User.CreateUser(Guid.NewGuid().ToString());
            var books = CreateABookList();

            user = user.SetListedBooks(books);

            user.ListedBooks.Should().HaveCount(books.Count);
            user.ListedBooks.SequenceEqual(books).Should().BeTrue();
        }

        [Fact]
        public void SetBorrowedInBooks_WithCollection_ShouldBeSameAsUserBorrowedInBooks()
        {
            var user = User.CreateUser(Guid.NewGuid().ToString());
            var books = CreateABookList();

            user = user.SetListedBooks(books);

            user.BorrowedIn.Should().HaveCount(books.Count);
            user.BorrowedIn.SequenceEqual(books).Should().BeTrue();
        }

        [Fact]
        public void SetBorrowedAwayBooks_WithCollection_ShouldBeSameAsUserBorrowedAwayBooks()
        {
            var user = User.CreateUser(Guid.NewGuid().ToString());
            var books = CreateABookList();

            user = user.SetListedBooks(books);

            user.BorrowedOut.Should().HaveCount(books.Count);
            user.BorrowedOut.SequenceEqual(books).Should().BeTrue();
        }

        [Fact]
        public void SetUserPassword_WithPasswordBytes_ShouldBeSameAsUserHashedPassword()
        {

        }

        [Fact]
        public void SetUserPassword_WithPasswordBytes_SaltShouldBeChanged()
        {

        }

        [Fact]
        public void SetUserPasswordHashAndSalt_WithPasswordBytesAndSaltBytes_ShouldBeSameAsUserHashedPasswordAndSalt()
        {

        }

        private static List<BookBase> CreateABookList()
        {
            var list = new List<BookBase>()
            {
                Book.CreateBook(Guid.NewGuid().ToString(), User.CreateUser(Guid.NewGuid().ToString())),
                Book.CreateBook(Guid.NewGuid().ToString(), User.CreateUser(Guid.NewGuid().ToString())),
                Book.CreateBook(Guid.NewGuid().ToString(), User.CreateUser(Guid.NewGuid().ToString()))
            };

            return list;
        }
    }
}
