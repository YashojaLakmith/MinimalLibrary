namespace Domain.BaseEntities
{
    public class Address
    {
        public string Street1 { get; }
        public string Street2 { get; }

        private Address(string street1, string street2)
        {
            Street1 = street1;
            Street2 = street2;
        }

        public static Address CreateAddress(string street1, string street2)
        {
            throw new NotImplementedException();
        }
    }
}
