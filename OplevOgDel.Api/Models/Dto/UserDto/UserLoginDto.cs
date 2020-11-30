using System.ComponentModel.DataAnnotations;

namespace OplevOgDel.Api.Models.Dto.UserDto
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "A usernermae is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "A password is required")]
        public string Password { get; set; }
    }
}
