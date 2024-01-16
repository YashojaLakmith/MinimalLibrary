using System.Text;

namespace Domain
{
    internal static class Utility
    {
        internal static byte[] GetPasswordBytes(string password)
        {
            return Encoding.ASCII.GetBytes(password);
        }
    }
}
