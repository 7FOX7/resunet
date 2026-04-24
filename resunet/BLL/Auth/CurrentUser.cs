namespace resunet.BLL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool IsLoggedIn()
        {
            // OLD: check session
            // if there's a session id stored that means a user is logged in
            //return httpContextAccessor.HttpContext?.Session.GetInt32(AuthConstants.SESSION_USER_ID) != null;  

            // if there's cookies that means a user is logged in
            return httpContextAccessor.HttpContext?.Request.Cookies.FirstOrDefault(m => m.Key == AuthConstants.SESSION_COOKIE_ID) != null; 
        }
    }
}
