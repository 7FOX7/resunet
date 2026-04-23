using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using resunet.ViewModels;
using System.Security.Cryptography;
using System.Text;

namespace resunet.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        [Route("/profile")]
        public IActionResult Index()
        {
            // create an empty view model
            return View("Index", new ProfileViewModel());
        }
    }
}
