using System.ComponentModel.DataAnnotations;

namespace resunet.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty; 

        // ViewModel should not know anything about a UserAuth model
    }
}