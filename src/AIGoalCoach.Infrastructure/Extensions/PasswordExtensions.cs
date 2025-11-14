using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AIGoalCoach.Infrastructure.Extensions
{
    public static class PasswordExtensions
    {
        public static string HashPassword(this string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }

        public static bool VerifyPassword(this string password, string hashedPassword)
        {
            var hashOfInput = password.HashPassword();
            return StringComparer.Ordinal.Compare(hashOfInput, hashedPassword) == 0;
        }
    }
}
