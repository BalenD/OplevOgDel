using System;

namespace OplevOgDel.Web.Models.Dto
{
    public class CreateReviewDto
    {
        public string Description { get; set; }
        public Guid ProfileId { get; set; }
        public Guid ExperienceId { get; set; }
    }
}
