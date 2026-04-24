using Microsoft.Data.SqlClient;
using resunet.DAL;
using resunet.DAL.Models;

namespace resunet.BLL.Auth
{
    public class DbSessionBLL : IDbSessionBLL
    {
        private readonly IDbSessionDAL dbSessionDAL;

        // TODO: when there are more services that need to be recalculated on each request, consider creating a separate service registered as scoped / transient. That service will contain all services you want to re-calculate on each request (like httpContextAccessor). You then inject that service here and in other controllers (controller are re-created on each request anyway, so that injected services will be re-created as well)
        private readonly IHttpContextAccessor httpContextAccessor;

        public DbSessionBLL(IDbSessionDAL dbSessionDAL, IHttpContextAccessor httpContextAccessor)
        {
            this.dbSessionDAL = dbSessionDAL;
            this.httpContextAccessor = httpContextAccessor;
        }

        private void CreateSessionCookie(int sessionId)
        {
            CookieOptions options = new CookieOptions();
            options.Path = "/";
            options.HttpOnly = true;
            options.Secure = true;
            // add expiration date that will be updated every time a user is actively interacting with out app
            // options.Expires = ??

            httpContextAccessor?.HttpContext?.Response.Cookies.Delete(AuthConstants.SESSION_COOKIE_ID);
            httpContextAccessor?.HttpContext?.Response.Cookies.Append(AuthConstants.SESSION_COOKIE_ID, sessionId.ToString(), options);
        }

        public async Task CreateSessionAsync(SessionModel sessionModel)
        {
            // create a session and get id of just created session
            int sessionId = await dbSessionDAL.CreateSession(sessionModel);

            // store session id in cookies
            CreateSessionCookie(sessionId); 
        }
    }
}
