using System.ComponentModel.DataAnnotations;

using Domain.Dto;

using FluentAssertions;

using Xunit;

namespace DomainTests.DtoTests
{
    public class PaginationDtoTests
    {
        [Fact]
        public void Validation_WithPageNoLessThan1_ShouldFail()
        {
            var pagination = new Pagination(-1, 10);
            var ctx = new ValidationContext(pagination);

            var act = () => Validator.ValidateObject(pagination, ctx, true);

            act.Should().Throw<ValidationException>();
        }

        [Fact]
        public void Validation_WithPageNoHigherThan32767_ShouldFail()
        {
            var pagination = new Pagination(99999, 10);
            var ctx = new ValidationContext(pagination);

            var act = () => Validator.ValidateObject(pagination, ctx, true);

            act.Should().Throw<ValidationException>();
        }

        [Fact]
        public void Validation_WithResultCountLessThan10_ShouldFail()
        {
            var pagination = new Pagination(1, 9);
            var ctx = new ValidationContext(pagination);

            var act = () => Validator.ValidateObject(pagination, ctx, true);

            act.Should().Throw<ValidationException>();
        }

        [Fact]
        public void Validation_WithResultCountHigherThan100_ShouldFail()
        {
            var pagination = new Pagination(1, 101);
            var ctx = new ValidationContext(pagination);

            var act = () => Validator.ValidateObject(pagination, ctx, true);

            act.Should().Throw<ValidationException>();
        }

        [Fact]
        public void Validation_WithPageNoNull_ShouldHaveDefaultValue1()
        {
            var pagination = new Pagination(ResultsCount:10);

            pagination.PageNo.Should().Be(1);
        }

        [Fact]
        public void Validation_WithResultCountNull_ShoulHaveDefaultValue10()
        {
            var pagination = new Pagination(1);
            
            pagination.ResultsCount.Should().Be(10);
        }
    }
}
