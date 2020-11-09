using OplevOgDel.Api.Data.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OplevOgDel.Api.Data.Models
{
    [Table("Ratings")]
    public class Rating : BaseModel
    {
        [Range(1, 5, ErrorMessage = "RateCount must be 1 to 5")]
        public int RatingCount { get; set; }
        public Guid ProfileId { get; set; }
        public Profile Creator { get; set; }
        public Guid ExperienceId { get; set; }
        public Experience Experience { get; set; }
    }
}
