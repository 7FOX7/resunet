using System.ComponentModel.DataAnnotations;

namespace resunet.ViewModels
{
    public class RegisterViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$%^&*-]).{10,}$", ErrorMessage = "Password is too easy to guess")]
        public string Password { get; set; } = string.Empty;

        // perform additional validation
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }

        // ViewModel should not know anything about a UserAuth model
    }
}