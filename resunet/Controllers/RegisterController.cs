using Microsoft.AspNetCore.Mvc;
using resunet.BLL.Auth;
using resunet.ViewModels;
using resunet.ViewMappers; 

namespace resunet.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAuthBLL authBLL; 

        public RegisterController(IAuthBLL authBLL)
        {
            this.authBLL = authBLL;
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
        public IActionResult IndexSave(RegisterViewModel registerViewModel)
        { 
            if (ModelState.IsValid)
            {
                authBLL.CreateUser(AuthMapper.MapRegisterViewModelToUserAuth(registerViewModel)); 
            }

            return View("Index", registerViewModel); 
        }
    }
}
