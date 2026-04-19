using Microsoft.AspNetCore.Mvc;
using resunet.BLL.Auth;
using resunet.Models;
using System.Diagnostics;

namespace resunet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICurrentUser currentUser;

        public HomeController(ICurrentUser currentUser)
        {
            this.currentUser = currentUser;
        }

        public IActionResult Index()
        {
            return View(currentUser.IsLoggedIn());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
