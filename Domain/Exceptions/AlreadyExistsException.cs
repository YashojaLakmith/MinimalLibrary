namespace Domain.Exceptions
{
    public class AlreadyExistsException(string argument) : Exception(argument)
    {
    }
}
