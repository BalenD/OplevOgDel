using OplevOgDel.Api.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Data.Models
{
    public class ExperienceReport : BaseModel
    {
        [Required(ErrorMessage = "A report description is required")]
        public string Description { get; set; }
        public Guid ProfileId { get; set; }
        public Profile Creator { get; set; }
        public Guid ExperienceId { get; set; }
        public Experience Experience { get; set; }
    }
}
