using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace NewProject.Authorization.Services
{
    public class TokenRepository : IToken
    {
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<string> CreateHash(string password)
        {
            // Generate a random salt
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[24];
            csprng.GetBytes(salt);

            // Hash the password and encode the parameters
            byte[] hash = await PBKDF2(password, salt, 1000, 24);
            return (1000 + ":" + Convert.ToBase64String(salt) + ":" +Convert.ToBase64String(hash));
        }

        private async Task<byte[]> PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            await Task.CompletedTask;
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }


    }
}
