using Microsoft.AspNetCore.Mvc;
using resunet.BLL.Auth;
using resunet.ViewModels;
using resunet.ViewMappers; 

namespace resunet.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAuthBLL authBLL;

        // TODO: when there are more services that need to be recalculated on each request, consider creating a separate service registered as scoped / transient. That service will contain all services you want to re-calculate on each request (like httpContextAccessor). You then inject that service here and in other controllers (controller are re-created on each request anyway, so that injected services will be re-created as well)
        private readonly IHttpContextAccessor httpContextAccessor;

        public RegisterController(IAuthBLL authBLL, IHttpContextAccessor httpContextAccessor)
        {
            this.authBLL = authBLL;
            this.httpContextAccessor = httpContextAccessor;
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
            if (ModelState.IsValid)
            {
                // get an id for creating a session
                int id = await authBLL.CreateUser(AuthMapper.MapRegisterViewModelToUserAuth(registerViewModel));

                Login(id); 
                return Redirect("/"); 
            }

            return View("Index", registerViewModel); 
        }

        private void Login(int id)
        {
            // create a session
            httpContextAccessor.HttpContext?.Session.SetInt32(AuthConstants.SESSION_USER_ID, id);
        }
    }
}
