namespace DegreeJobTracker.Models.Validation
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class PasswordHasher
    {

        private static readonly string _salt = "V74L9cCpU0\\&U|`(a><:J;`+";

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the password string to a byte array
                byte[] bytes = Encoding.UTF8.GetBytes($"{password}{_salt}");

                // Compute the hash
                byte[] hashBytes = sha256.ComputeHash(bytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
