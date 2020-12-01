using System.ComponentModel.DataAnnotations;

namespace OplevOgDel.Web.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Brugernavn er krævet!")]
        [StringLength(maximumLength: 100, MinimumLength = 10, ErrorMessage = "Dit brugernavn skal være imellem 10 og 100 tegn.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Kodeord er krævet!")]
        [StringLength(maximumLength: 100, MinimumLength = 10, ErrorMessage = "Dit kodeord skal være imellem 10 og 100 tegn.")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
            ErrorMessage = "Dit kodeord skal mindst være på 10 tegn, og indeholde mindst 3 ud af de 4 følgende: Et stort bogstav (A-Z), et lille bogstav (a-z), et tal (0-9) eller et specielt tegn (f.eks. !@#$%^&*).")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email er krævet!")]
        [EmailAddress(ErrorMessage = "Email skal være en valid email.")]
        public string Email { get; set; }
    }
}
