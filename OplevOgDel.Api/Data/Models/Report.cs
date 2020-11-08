using OplevOgDel.Api.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace OplevOgDel.Api.Data.Models
{
    public class Report : BaseModel
    {
        [Required( ErrorMessage = "A report description is required")]
        public string Description { get; set; }
        public Profile Owner { get; set; }
        public Review Review { get; set; }
    }
}
