using System.ComponentModel.DataAnnotations;

namespace OplevOgDel.Api.Models.Dto.UserDto
{
    public class UpdateUserDto
    {
        [StringLength(maximumLength: 100, MinimumLength = 10, ErrorMessage = "Username Must be Less than 100 characters and more than 10 characters")]
        public string Username { get; set; }
        [StringLength(maximumLength: 100, MinimumLength = 10, ErrorMessage = "Password Must be Less than 100 characters and more than 10 characters")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Password must be at least 10 characters and contain at least 3 out of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }
        [EmailAddress(ErrorMessage = "Email is nto a valid email address")]
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
