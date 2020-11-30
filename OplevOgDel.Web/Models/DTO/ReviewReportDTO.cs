using System;

namespace OplevOgDel.Web.Models.Dto
{
    public class ReviewReportDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid ProfileId { get; set; }
        public ProfileDto Creator { get; set; }
        public Guid ExperienceId { get; set; }
        public ReviewDto Review { get; set; }
    }
}