using resunet.DAL;
using resunet.DAL.Models;

namespace resunet.BLL.Auth
{
    public interface IDbSessionBLL : IDbSessionDAL
    {
        Task CreateSessionAsync(SessionModel sessionModel);
    }
}
