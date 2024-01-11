namespace Domain.BaseEntities
{
    public class Address
    {
        public string Street1 { get; }
        public string Street2 { get; }

        public Address(string street1, string street2)
        {
            Street1 = street1;
            Street2 = street2;
        }
    }
}
