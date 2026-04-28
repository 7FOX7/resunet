using resunet.DAL;
using resunet.DAL.Models;
using resunet.Exceptions; 

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

        public async Task<int> Authenticate(string email, string password, bool rememberMe)
        {
            // try to get a user with the given email
            UserAuth user = await GetUser(email);

            // if passwords don't match
            if (user.UserID is not null && user.Password == encrypt.HashPassword(password, user.Salt))
            {
                return user.UserID ?? 0; 
            }

            throw new AuthorizationException();
        }

        // this method combines two other methods for registering a new user: email validation and creating a new user
        public async Task<int> Register(UserAuth userAuth)
        {
            // TODO: you need to make sure you have a variable assigned here because 'ValidateEmail' returns validation result / null
            await ValidateEmail(userAuth.Email);
            // return an id of just created user
            return await CreateUser(userAuth); 
        }

        // create a user based on the model and return their id
        public async Task<int> CreateUser(UserAuth userAuth)
        {
            // unique salt
            userAuth.Salt = Guid.NewGuid().ToString();
            userAuth.Password = encrypt.HashPassword(userAuth.Password, userAuth.Salt);

            return await authDAL.CreateUser(userAuth);
        }

        public async Task ValidateEmail(string email)
        {
            UserAuth user = await GetUser(email);
            
            // duplicate email
            if (user.UserID is not null)
            {
                throw new DuplicateEmailException(); 
            }
        }
    }
}
