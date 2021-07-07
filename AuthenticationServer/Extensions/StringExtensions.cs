using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AuthenticationServer.Extensions
{
    public static class StringExtentions
    {
        private const int NUMBER_OF_ROUNDS = 1000;
        public static byte[] ToPasswordHash(this string self, byte[] salt)
        {
            using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(self, salt, NUMBER_OF_ROUNDS);
            return rfc2898DeriveBytes.GetBytes(20);
        }
    }
}
