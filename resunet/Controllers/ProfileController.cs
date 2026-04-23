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

        [HttpPost]
        [Route("/profile")]
        public async Task<IActionResult> IndexSave(ProfileViewModel profileViewModel)
        {
            // will only be executed if all creds are valid (profile name, first name, last name, etc.)
            if (ModelState.IsValid && Request.Form.Files.Count > 0)
            {
                // get the file 
                IFormFile file = Request.Form.Files[0];

                // create md5 hash algorithm
                MD5 md5Hash = MD5.Create();
                byte[] hashBytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(file.FileName));

                // get a hex string
                string hexString = Convert.ToHexString(hashBytes);
                // build a dir
                string dir = new StringBuilder()
                    .Append("./wwwroot")
                    .Append("/images/")
                    .Append($"{hexString.Substring(0, 2)}")          // folder name (2 chars)
                    .ToString();

                // make sure there's no directory
                Directory.CreateDirectory(dir); 

                // create a file path which is a combination of wwwroot directory + file name
                string filePath = new StringBuilder()
                    .Append(dir)
                    .Append("/")
                    .Append(hexString.Substring(3, 4))
                    .Append("-")
                    .Append(file.FileName)
                    .ToString();

                // write to a file
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream); 
                }
            }

            return View("Index", profileViewModel);
        }
    }
}
