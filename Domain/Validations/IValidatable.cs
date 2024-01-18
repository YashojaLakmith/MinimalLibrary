using Domain.Exceptions;

namespace Domain.Validations
{
    public interface IValidatable
    {
        /// <summary>
        /// Validates the state of the object.
        /// </summary>
        /// <exception cref="ValidationFailedException"/>
        void Validate();
    }
}
