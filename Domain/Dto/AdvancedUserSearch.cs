using Domain.Exceptions;
using Domain.Validations;

namespace Domain.Dto
{
    public record AdvancedUserSearch(int PageNo = 1, int ResultCount = 100, string? UserName = null) : Pagination(PageNo, ResultCount), IValidatable
    {
        public new void Validate()
        {
            base.Validate();            
            if (UserName is not null && UserName.Length > 200) throw new ValidationFailedException("User name cannot exceed 200 characters.");
        }
    }
}
