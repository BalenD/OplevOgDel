using OplevOgDel.Api.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OplevOgDel.Api.Data.Models
{
    [Table("Reviews")]
    public class Review : BaseModel
    {
        [Required(ErrorMessage = "A review description is required")]
        public string Description { get; set; }
        public Profile Owner { get; set; }
        public Experience Experience { get; set; }
    }
}
