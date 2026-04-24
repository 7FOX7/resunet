using Microsoft.AspNetCore.Mvc;
using resunet.BLL.Auth;
using resunet.ViewModels;
using resunet.ViewMappers;
using resunet.DAL.Models; 

namespace resunet.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAuthBLL authBLL;

        // TODO: when there are more services that need to be recalculated on each request, consider creating a separate service registered as scoped / transient. That service will contain all services you want to re-calculate on each request (like httpContextAccessor). You then inject that service here and in other controllers (controller are re-created on each request anyway, so that injected services will be re-created as well)
        private readonly IHttpContextAccessor httpContextAccessor;

        private readonly IDbSessionBLL dbSessionBLL; 

        public RegisterController(IAuthBLL authBLL, IHttpContextAccessor httpContextAccessor, IDbSessionBLL dbSessionBLL)
        {
            this.authBLL = authBLL;
            this.httpContextAccessor = httpContextAccessor;
            this.dbSessionBLL = dbSessionBLL;
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Index()
        {
            // create an empty view model
            return View("Index", new RegisterViewModel());    
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> IndexSave(RegisterViewModel registerViewModel)
        {
            // will only be executed if all creds are valid (email, password, etc.)
            if (ModelState.IsValid)
            {
                var result = await authBLL.ValidateEmail(registerViewModel.Email); 

                // try to display an error
                if (result is not null)
                {
                    ModelState.TryAddModelError("Email", result.ErrorMessage!); 
                }
            }

            // will only be executed if all creds are valid (email, password, etc.)
            if (ModelState.IsValid)
            {
                // get an id for creating a session
                int id = await authBLL.CreateUser(AuthMapper.MapRegisterViewModelToUserAuth(registerViewModel));

                // create a session and pass information
                await dbSessionBLL.CreateSessionAsync(new SessionModel()
                {
                    CreatedAt = DateTime.Now,
                    LastAccessedAt = DateTime.Now,
                    UserID = id
                }); 

                return Redirect("/"); 
            }

            return View("Index", registerViewModel); 
        }



        // OLD - using Microsoft session
        //private void Login(int id)
        //{
        //    // create a session
        //    httpContextAccessor.HttpContext?.Session.SetInt32(AuthConstants.SESSION_USER_ID, id);
        //}
    }
}
