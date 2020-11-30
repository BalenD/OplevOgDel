using System;

namespace OplevOgDel.Api.Models.Dto.UserDto
{
    public class ViewUserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
