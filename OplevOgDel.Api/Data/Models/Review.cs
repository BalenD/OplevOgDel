using OplevOgDel.Api.Data.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OplevOgDel.Api.Data.Models
{
    [Table("Reviews")]
    public class Review : BaseModel
    {
        [Required(ErrorMessage = "A review description is required")]
        public string Description { get; set; }
        public Guid ProfileId { get; set; }

        public Profile Creator { get; set; }
        public Guid ExperienceId { get; set; }
        public Experience Experience { get; set; }
    }
}
