using System.ComponentModel.DataAnnotations;

namespace OplevOgDel.Api.Models.Dto.UserDto
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
