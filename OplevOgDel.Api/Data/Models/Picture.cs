using OplevOgDel.Api.Data.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OplevOgDel.Api.Data.Models
{
    [Table("pictures")]
    public class Picture : BaseModel
    {
        [Required]
        public string Path { get; set; }
        public Guid CreatorId { get; set; }
        public Profile Creator { get; set; }
        public Guid ExperienceId { get; set; }
        public Experience Experience { get; set; }
    }
}
