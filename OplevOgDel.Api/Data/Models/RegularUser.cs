using System.ComponentModel.DataAnnotations.Schema;

namespace OplevOgDel.Api.Data.Models
{
    [Table("RegularUsers")]
    public class RegularUser : User
    {
        public Profile AccountOwner { get; set; }
    }
}
