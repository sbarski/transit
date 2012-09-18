using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Transit.Infrastructure.Security
{
    public class SecurityHelper
    {
        public static string GeneratePasswordHash(string password)
        {
            return HashHelper.GetHash(password);
        }

        public static string GenerateSalt()
        {
            return GetUniqueKey();
        }

        public static string GenerateSecurityToken()
        {
            return GetUniqueKey();
        }

        private static string GetUniqueKey(int maxSize = 20)
        {
            char[] chars = new char[62];
            chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];

            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);

            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
