using resunet.DAL.Models;

namespace resunet.DAL
{
    public interface IAuthDAL
    {
        Task<UserAuth> GetUser(string email);
        
        Task<UserAuth> GetUser(int id);

        // create a user based on the model and return their id
        Task<int> CreateUser(UserAuth userAuth); 
    }
}
