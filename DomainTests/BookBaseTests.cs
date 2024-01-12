using Domain;

using FluentAssertions;

using Xunit;

namespace DomainTests
{
    public class BookBaseTests
    {
        [Fact]
        public void Equals_ForBookWithSameId_ShouldReturnTrue()
        {
            var id = Guid.NewGuid().ToString();
            var book1 = Book.CreateBook(id, Guid.NewGuid().ToString(), CreateRandomUser());
            var book2 = Book.CreateBook(id, Guid.NewGuid().ToString(), CreateRandomUser());

            bool result = book1.Equals(book2);

            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_ForBookWithDifferentId_ShouldReturnFalse()
        {
            var id1 = Guid.NewGuid().ToString();
            var id2 = Guid.NewGuid().ToString();
            var book1 = Book.CreateBook(id1, Guid.NewGuid().ToString(), CreateRandomUser());
            var book2 = Book.CreateBook(id2, Guid.NewGuid().ToString(), CreateRandomUser());

            bool result = book1.Equals(book2);

            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_WithNonChild_ShouldReturnFalse()
        {
            var book = Book.CreateBook(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), CreateRandomUser());
            var any = new object();

            bool result = book.Equals(any);

            result.Should().BeFalse();
        }

        [Fact]
        public void EqualOperator_WithBookWithSameId_ShouldReturnTrue()
        {
            var id = Guid.NewGuid().ToString();
            var book1 = Book.CreateBook(id, Guid.NewGuid().ToString(), CreateRandomUser());
            var book2 = Book.CreateBook(id, Guid.NewGuid().ToString(), CreateRandomUser());

            bool result = book1 == book2;

            result.Should().BeTrue();
        }

        [Fact]
        public void EqualOperator_WithUserWithDifferentId_ShouldReturnFalse()
        {
            var id1 = Guid.NewGuid().ToString();
            var id2 = Guid.NewGuid().ToString();
            var book1 = Book.CreateBook(id1, Guid.NewGuid().ToString(), CreateRandomUser());
            var book2 = Book.CreateBook(id2, Guid.NewGuid().ToString(), CreateRandomUser());

            bool result = book1 == book2;

            result.Should().BeFalse();
        }

        [Fact]
        public void InequalOperator_WithUserWithSameId_ShouldReturnFalse()
        {
            var id = Guid.NewGuid().ToString();
            var book1 = Book.CreateBook(id, Guid.NewGuid().ToString(), CreateRandomUser());
            var book2 = Book.CreateBook(id, Guid.NewGuid().ToString(), CreateRandomUser());

            bool result = book1 != book2;

            result.Should().BeFalse();
        }

        [Fact]
        public void InequalOperator_WithUserWithDifferentId_ShouldReturnTrue()
        {
            var id1 = Guid.NewGuid().ToString();
            var id2 = Guid.NewGuid().ToString();
            var book1 = Book.CreateBook(id1, Guid.NewGuid().ToString(), CreateRandomUser());
            var book2 = Book.CreateBook(id2, Guid.NewGuid().ToString(), CreateRandomUser());

            bool result = book1 != book2;

            result.Should().BeTrue();
        }

        private static User CreateRandomUser()
        {
            return User.CreateUser(Guid.NewGuid().ToString());
        }
    }
}
