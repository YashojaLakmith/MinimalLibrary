using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

using Domain.Dto;

using FluentAssertions;

using Xunit;

namespace DomainTests.DtoTests
{
    public class BookSearchDtoTests
    {
        [Fact]
        public void Validation_WhenBookNameLengthHigherThan200_ShouldFail()
        {
            var randomString = Convert.ToHexString(RandomNumberGenerator.GetBytes(101));
            var dto = new AdvancedBookSearch(BookName: randomString);
            var ctx = new ValidationContext(dto);

            var act = () => Validator.ValidateObject(dto, ctx, true);

            act.Should().Throw<ValidationException>();
        }

        [Fact]
        public void Validation_WhenAuthorNameLengthHigherThan200_ShouldFail()
        {
            var randomString = Convert.ToHexString(RandomNumberGenerator.GetBytes(101));
            var dto = new AdvancedBookSearch(AuthorName: randomString);
            var ctx = new ValidationContext(dto);

            var act = () => Validator.ValidateObject(dto, ctx, true);

            act.Should().Throw<ValidationException>();
        }
    }
}
