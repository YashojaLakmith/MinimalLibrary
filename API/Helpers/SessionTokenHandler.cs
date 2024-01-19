using System.Security.Cryptography;

namespace API.Helpers
{
    public static class SessionTokenHandler
    {
        public static string GenerarateASessionToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToHexString(bytes);
        }

        /// <exception cref="ArgumentOutOfRangeException"/>
        public static string GetTokenWithSpecificLength(int length)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(length, 1);

            var bytes = RandomNumberGenerator.GetBytes(length);
            return Convert.ToHexString(bytes);
        }

        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <param name="length">must be between 1 and 9.</param>
        public static int GetARandomNumber(int length)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(length, 1);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(length, 9);

            var max = (int)Math.Pow(10, length);

            return RandomNumberGenerator.GetInt32(max) % max;
        }

        ///<exception cref="FormatException"/>
        public static bool CompareSessionToken(string token1, string token2)
        {
            try
            {
                var bytes1 = Convert.FromHexString(token1);
                var bytes2 = Convert.FromHexString(token2);

                return bytes1.Length == bytes2.Length && bytes1.SequenceEqual(bytes2);
            }
            catch (FormatException)
            {
                throw;
            }
        }
    }
}
