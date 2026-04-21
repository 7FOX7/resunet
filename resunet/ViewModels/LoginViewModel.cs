using System.ComponentModel.DataAnnotations;

namespace resunet.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty; 

        // bool is non nullable
        public bool RememberMe { get; set; }

        // ViewModel should not know anything about a UserAuth model
    }
}
