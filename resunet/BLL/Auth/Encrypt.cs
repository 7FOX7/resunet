using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text; 

namespace resunet.BLL.Auth
{
    public class Encrypt : IEncrypt
    {
        public string HashPassword(string password, string salt)
        {
            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password, 
                    Encoding.ASCII.GetBytes(salt), 
                    KeyDerivationPrf.HMACSHA256, 
                    500, 
                    32
                )
            ); 
        }
    }
}
