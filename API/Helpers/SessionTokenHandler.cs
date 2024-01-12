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
