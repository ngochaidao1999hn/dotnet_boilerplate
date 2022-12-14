using System.Security.Cryptography;
using System.Text;

namespace Application.Helpers
{
    public class PasswordHelper
    {
        public string HashPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashValue = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToHexString(hashValue);
            }
        }
    }
}