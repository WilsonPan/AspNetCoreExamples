using System.Security.Cryptography;
using System.Text;

namespace JwtAuth
{
    public class JwtHashHelper
    {
        public static byte[] GetHash(string inputString)
        {
            var base64String = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(inputString));

            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(base64String));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}