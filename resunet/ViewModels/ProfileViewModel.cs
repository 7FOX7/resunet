using System.ComponentModel.DataAnnotations;

namespace resunet.ViewModels
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "Profile name is required")]
        public string ProfileName { get; set; } = string.Empty;

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = string.Empty;

        // ViewModel should not know anything about a UserAuth model
    }
}
