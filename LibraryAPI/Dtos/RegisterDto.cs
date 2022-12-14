using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "UserName is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
