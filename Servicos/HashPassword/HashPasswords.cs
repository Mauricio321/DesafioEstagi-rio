using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.HashPassword
{
    public class HashPasswords
    {
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public string HashPassword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            return HashPassword(password, salt);
        }

        public string HashPassword(string password, byte[] salt)
        {
            var hash = Rfc2898DeriveBytes.Pbkdf2(
               Encoding.UTF8.GetBytes(password),
               salt,
               iterations,
               hashAlgorithm,
               keySize);

            return Convert.ToHexString(hash);
        }

        public bool Comparer(string password, byte[] salt, string hashedPasswordToComparer)
        {
            var hashedPassword = HashPassword(password, salt);

            return hashedPassword == hashedPasswordToComparer;
        }
    }
}
