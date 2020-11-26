using System.ComponentModel.DataAnnotations;

namespace OplevOgDel.Api.Models.Dto.UserDto
{
    public class UpdateUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
