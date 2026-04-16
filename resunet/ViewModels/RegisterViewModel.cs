namespace resunet.ViewModels
{
    public class RegisterViewModel
    {
        public string? Email { get; set; }

        public string? Password { get; set; } 

        // ViewModel should not know anything about a UserAuth model
    }
}