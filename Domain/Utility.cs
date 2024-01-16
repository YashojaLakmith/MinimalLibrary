using System.Text;

namespace Domain
{
    internal static class Utility
    {
        internal static byte[] GetPasswordBytes(string password)
        {
            return Encoding.ASCII.GetBytes(password);
        }

        internal static (int, int) GetSkipAndTake(int pageNo, int recordsPerPage)
        {
            int skip = (pageNo - 1) * recordsPerPage;

            return (skip, recordsPerPage);
        }
    }
}
