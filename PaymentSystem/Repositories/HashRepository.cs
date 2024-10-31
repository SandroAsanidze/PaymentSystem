using PaymentSystem.Controllers;
using PaymentSystem.Interface;
using PaymentSystem.Models.Deposit;
using PaymentSystem.Models.Transaction;
using PaymentSystem.Models.Withdraw;
using System.Security.Cryptography;
using System.Text;

namespace PaymentSystem.Repositories
{
    public class HashRepository : IHashValidation
    {
        private const int SaltSize = 128 / 8;
        private const int KeySize = 256 / 8;
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        private const char Delimiter = ';';
        public string Hash(string info)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(info, salt, Iterations, _hashAlgorithmName, KeySize);

            return string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public bool Verify(string infoHash, string info)
        {
            var elements = infoHash.Split(Delimiter);
            var salt = Convert.FromBase64String(elements[0]);
            var hash = Convert.FromBase64String(elements[1]);

            var hashInput = Rfc2898DeriveBytes.Pbkdf2(info,salt,Iterations, _hashAlgorithmName,KeySize);

            return CryptographicOperations.FixedTimeEquals(hash, hashInput);
        }
    }
}
