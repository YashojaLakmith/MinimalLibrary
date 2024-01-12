namespace Domain.Exceptions
{
    public class RecordNotFoundException(string record) : Exception(record)
    {
    }
}
