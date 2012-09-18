using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Transit.Infrastructure.Security
{
    public class HashHelper
    {
        public enum HashType
        {
            MD5,
            SHA1,
            SHA256,
            SHA512
        }

        public static string GetHash(string text, HashType hashType = HashType.SHA512)
        {
            HashAlgorithm hashAlgorithm = null;

            switch (hashType)
            {
                case HashType.MD5:
                    hashAlgorithm = new MD5CryptoServiceProvider();
                    break;

                case HashType.SHA1:
                    hashAlgorithm = new SHA1Managed();
                    break;

                case HashType.SHA256: 
                    hashAlgorithm = new SHA256Managed();
                    break;

                case HashType.SHA512:
                    hashAlgorithm = new SHA512Managed();
                    break;
            }

            return ComputeHash(text, hashAlgorithm);
        }

        public static bool CompareHash(string original, string hashString, HashType hashType = HashType.SHA512)
        {
            var originalHash = GetHash(original, hashType);
            return (originalHash == hashString);
        }

        private static string ComputeHash(string text, HashAlgorithm hashAlgorithm)
        {
            var hash = hashAlgorithm.ComputeHash(UnicodeEncoding.UTF8.GetBytes(text));

            return UnicodeEncoding.UTF8.GetString(hash);
        }
    }
}
