using resunet.DAL.Models;
using resunet.ViewModels;

namespace resunet.ViewMappers
{
    public class AuthMapper
    {
        public static UserAuth MapRegisterViewModelToUserAuth(RegisterViewModel registerViewModel)
        {
            UserAuth userAuth = new UserAuth
            {
                // we guarantee that fields are NOT null (validation for nullability will be made in Controller methods)
                Email = registerViewModel.Email!, 
                Password = registerViewModel.Password!
            };

            return userAuth; 
        }
    }
}
