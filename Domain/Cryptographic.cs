using System.Security.Cryptography;

namespace Domain
{
    /// <summary>
    /// Defines common cryptographic functionalities for the application.
    /// </summary>
    public class Cryptographic
    {
        public static byte[] GeneratePassword(byte[] pw, byte[] salt, int length)
        {
            ArgumentNullException.ThrowIfNull(pw);
            ArgumentNullException.ThrowIfNull(salt);

            if(length < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            return Rfc2898DeriveBytes.Pbkdf2(pw, salt, 500000, HashAlgorithmName.SHA512, length);
        }
    }
}
