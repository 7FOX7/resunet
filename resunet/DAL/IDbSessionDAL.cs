using resunet.DAL.Models;

namespace resunet.DAL
{
    public interface IDbSessionDAL
    {
        // will be called when a user successfully registers / logins. We'll be creating a session and add its id to cookies
        Task<int> CreateSession(SessionModel sessionModel);

        // will be called when we want to update (extend) a session for the current user

        // I think this method should not return anything because it will be called at the same time we extend our cookies - session id already exists
        Task UpdateSession(int sessionId, DateTime createdAt, DateTime lastAccessedAt); 
    }
}
