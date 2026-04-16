using Dapper;
using Microsoft.Data.SqlClient;
using resunet.DAL;
using resunet.DAL.Models;

namespace resunet.BLL.Auth
{
    // high-level methods we can access directly from controllers, executing more "detailed" methods from DAL 
    public class AuthBLL : IAuthBLL
    {
        private readonly IAuthDAL authDAL; 

        public AuthBLL(IAuthDAL authDAL)
        {
            this.authDAL = authDAL;    
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
            return await authDAL.CreateUser(userAuth); 
        }
    }
}
