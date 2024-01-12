namespace Domain.BaseEntities
{
    /// <summary>
    /// Represents an address of a local user.
    /// </summary>
    public class Address : IEquatable<Address>
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
            if (string.IsNullOrEmpty(street1))
            {
                throw new ArgumentException(nameof(street1));
            }
            return new Address(street1, street2);
        }

        public bool Equals(Address? other)
        {
            if(other is null)
            {
                return false;
            }

            return Street1 == other.Street1 && Street2 == other.Street2;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Address);
        }

        public override int GetHashCode()
        {
            return Street1.GetHashCode() ^ Street2.GetHashCode();
        }

        public static bool operator ==(Address? left, Address? right)
        {
            if (left is null)
            {
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(Address? left, Address? right)
        {
            return !(left == right);
        }
    }
}
