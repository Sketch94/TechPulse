using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechPulse.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Du måste fylla i fältet.")]
        [EmailAddress(ErrorMessage = "Ogiltig e-postadress.")]
        [DisplayName("E-post")]
        public string? Username { get; set; } // Email address of the user


        [Required(ErrorMessage = "Du måste fylla i fältet.")]
        [DisplayName("Lösenord")]
        public string? Password { get; set; } // Password for the user account
    }
}
