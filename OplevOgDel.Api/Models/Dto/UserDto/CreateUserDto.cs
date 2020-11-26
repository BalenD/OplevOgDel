using System.ComponentModel.DataAnnotations;

namespace OplevOgDel.Api.Models.Dto.UserDto
{
    public class CreateUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
