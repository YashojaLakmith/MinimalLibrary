using Domain;

using FluentAssertions;

using Xunit;

namespace DomainTests
{
    public class UserBaseTests
    {
        [Fact]
        public void Equals_ForUserWithSameId_ShouldReturnTrue()
        {
            var id = Guid.NewGuid().ToString();
            var user1 = User.CreateUser(id);
            var user2 = User.CreateUser(id);

            bool result = user1.Equals(user2);

            result.Should().BeTrue();                                    
        }

        [Fact]
        public void Equals_ForUserWithDifferentId_ShouldReturnFalse()
        {
            var id1 = Guid.NewGuid().ToString();
            var id2 = Guid.NewGuid().ToString();
            var user1 = User.CreateUser(id1);
            var user2 = User.CreateUser(id2);

            bool result = user1.Equals(user2);

            result.Should().BeFalse();                                    
        }

        [Fact]
        public void Equals_WithNonChild_ShouldReturnFalse()
        {
            var id1 = Guid.NewGuid().ToString(); ;
            var user1 = User.CreateUser(id1);
            var any = new object();

            bool result = user1.Equals(any);

            result.Should().BeFalse();
        }

        [Fact]
        public void EqualOperator_WithUserWithSameId_ShouldReturnTrue()
        {
            var id1 = Guid.NewGuid().ToString();
            var user1 = User.CreateUser(id1);
            var user2 = User.CreateUser(id1);

            bool result = user1 == user2;

            result.Should().BeTrue();
        }

        [Fact]
        public void EqualOperator_WithUserWithDifferentId_ShouldReturnFalse()
        {
            var id1 = Guid.NewGuid().ToString();
            var id2 = Guid.NewGuid().ToString();
            var user1 = User.CreateUser(id1);
            var user2 = User.CreateUser(id2);

            bool result = user1 == user2;

            result.Should().BeFalse();
        }

        [Fact]
        public void InequalOperator_WithUserWithSameId_ShouldReturnFalse()
        {
            var id1 = Guid.NewGuid().ToString();
            var user1 = User.CreateUser(id1);
            var user2 = User.CreateUser(id1);

            bool result = user1 != user2;

            result.Should().BeFalse();
        }

        [Fact]
        public void InequalOperator_WithUserWithDifferentId_ShouldReturnTrue()
        {
            var id1 = Guid.NewGuid().ToString();
            var id2 = Guid.NewGuid().ToString();
            var user1 = User.CreateUser(id1);
            var user2 = User.CreateUser(id2);

            bool result = user1 != user2;

            result.Should().BeTrue();
        }
    }
}