using resunet.DAL;
using System.ComponentModel.DataAnnotations;

namespace resunet.BLL.Auth 
{
    // Auth BLL inherits all methods from Auth DAL but implements them differently (more high-level without writing queries)
    public interface IAuthBLL : IAuthDAL 
    {
        // 'Authenticate' must live in BLL (not DAL) because we are not interacting with the database directly
        Task<int> Authenticate(string email, string password, bool rememberMe);

        Task<ValidationResult?> ValidateEmail(string email); 
    }
}
