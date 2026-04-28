using Microsoft.AspNetCore.Mvc;
using resunet.BLL.Auth;
using resunet.DAL.Models;
using resunet.Exceptions;
using resunet.ViewMappers;
using resunet.ViewModels;

namespace resunet.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthBLL authBLL;

        // TODO: when there are more services that need to be recalculated on each request, consider creating a separate service registered as scoped / transient. That service will contain all services you want to re-calculate on each request (like httpContextAccessor). You then inject that service here and in other controllers (controller are re-created on each request anyway, so that injected services will be re-created as well)
        private readonly IHttpContextAccessor httpContextAccessor;

        public LoginController(IAuthBLL authBLL, IHttpContextAccessor httpContextAccessor)
        {
            this.authBLL = authBLL;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            // create an empty view model
            return View("Index", new LoginViewModel());
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> IndexSave(LoginViewModel loginViewModel)
        {
            // check for boolean value (= check if it defaults to 'false' when null)
            if (ModelState.IsValid)
            {
                try
                {
                    int id = await authBLL.Authenticate(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe);

                    Login(id);
                    return Redirect("/");
                }
                catch (AuthorizationException)
                {
                    ModelState.TryAddModelError("Email", "Email or password is invalid"); 
                }
                catch
                {
                    // TODO
                }
            }

            return View("Index", loginViewModel);
        }

        private void Login(int id)
        {
            // create a session
            httpContextAccessor.HttpContext?.Session.SetInt32(AuthConstants.SESSION_USER_ID, id);
        }
    }
}
