using OplevOgDel.Api.Data.Base;
using OplevOgDel.Api.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace OplevOgDel.Api.Data.Models
{
    [Table("AdminUsers")]
    public class User : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        //public UserRole Role { get; set; }
    }
}
