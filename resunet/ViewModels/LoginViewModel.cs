using System.ComponentModel.DataAnnotations;

namespace resunet.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty; 

        // bool is non nullable
        public bool RememberMe { get; set; }

        // ViewModel should not know anything about a UserAuth model
    }
}
