using resunet.DAL;
using resunet.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace resunet.BLL.Auth
{
    // high-level methods we can access directly from controllers, executing more "detailed" methods from DAL 
    public class AuthBLL : IAuthBLL
    {
        private readonly IAuthDAL authDAL;

        private readonly IEncrypt encrypt;

        public AuthBLL(IAuthDAL authDAL, IEncrypt encrypt)
        {
            this.authDAL = authDAL;    
            this.encrypt = encrypt;
        }

        public async Task<UserAuth> GetUser(string email)
        {
            return await authDAL.GetUser(email);
        }

        public async Task<UserAuth> GetUser(int id)
        {
            return await authDAL.GetUser(id);
        }

        // create a user based on the model and return their id
        public async Task<int> CreateUser(UserAuth userAuth)
        {
            // unique salt
            userAuth.Salt = Guid.NewGuid().ToString();
            userAuth.Password = encrypt.HashPassword(userAuth.Password, userAuth.Salt); 

            return await authDAL.CreateUser(userAuth);
        }

        public async Task<int> Authenticate(string email, string password, bool rememberMe)
        {
            // try to get a user with the given email
            UserAuth user = await GetUser(email);

            // if user is not found
            if (user.UserID is null)
            {
                throw new ArgumentException($"A user with email {email} doesn't exist"); 
            }

            // if passwords don't match
            if (user.Password != encrypt.HashPassword(password, user.Salt))
            {
                throw new Exception();
            }

            return user.UserID ?? 0; 
        }

        public async Task<ValidationResult?> ValidateEmail(string email)
        {
            UserAuth user = await GetUser(email);
            
            // duplicate email
            if (user.UserID is not null)
            {
                return new ValidationResult("User already exists"); 
            }

            return null; 
        }
    }
}
