using resunet.DAL; 

namespace resunet.BLL.Auth 
{
    // Auth BLL inherits all methods from Auth DAL but implements them differently (more high-level without writing queries)
    public interface IAuthBLL : IAuthDAL { }
}
