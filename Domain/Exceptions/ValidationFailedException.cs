namespace Domain.Exceptions
{
    public class ValidationFailedException(string failedArgument) : Exception(failedArgument)
    {
    }
}
