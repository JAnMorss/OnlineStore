using System.Security.Cryptography;
using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Users.ValueObjects
{
    public sealed class PasswordHash : ValueObject
    {
        public string Value { get; }

        private PasswordHash(string value)
        {
            Value = value;
        }

        private const int SaltSize = 16;
        private const int KeySize = 32; 
        private const int Iterations = 100_000;

        public static PasswordHash FromHashed(string hashedPassword)
        {
            return new PasswordHash(hashedPassword);
        }

        public static Result<PasswordHash> FromPlainText(string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(plainPassword))
            {
                return Result.Failure<PasswordHash>(new Error(
                    "Password.Empty",
                    "Password cannot be empty."));
            }

            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);

            var hash = HashPassword(plainPassword, salt);

            var combined = new byte[SaltSize + KeySize];
            Buffer.BlockCopy(salt, 0, combined, 0, SaltSize);
            Buffer.BlockCopy(hash, 0, combined, SaltSize, KeySize);

            string base64Hash = Convert.ToBase64String(combined);
            return new PasswordHash(base64Hash);
        }

        public bool Verify(string plainPassword)
        {
            var combined = Convert.FromBase64String(Value);

            byte[] salt = new byte[SaltSize];
            Buffer.BlockCopy(combined, 0, salt, 0, SaltSize);

            byte[] storedHash = new byte[KeySize];
            Buffer.BlockCopy(combined, SaltSize, storedHash, 0, KeySize);

            byte[] hashToCompare = HashPassword(plainPassword, salt);

            return CryptographicOperations.FixedTimeEquals(storedHash, hashToCompare);
        }

        private static byte[] HashPassword(string password, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            return pbkdf2.GetBytes(KeySize);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
