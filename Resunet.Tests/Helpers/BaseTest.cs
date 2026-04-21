using resunet.BLL.Auth;
using resunet.DAL;

namespace Resunet.Tests.Helpers
{
    public class BaseTest
    {
        protected static readonly IAuthDAL authDAL = new AuthDAL();
        
        protected static readonly IEncrypt encrypt = new Encrypt();

        protected static readonly IAuthBLL authBLL = new AuthBLL(authDAL, encrypt);
    }
}
