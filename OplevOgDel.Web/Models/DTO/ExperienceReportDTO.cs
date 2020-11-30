using System;

namespace OplevOgDel.Web.Models.Dto
{
    public class ExperienceReportDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid ProfileId { get; set; }
        public ProfileDto Creator { get; set; }
        public Guid ExperienceId { get; set; }
        public ExperienceDto Experience { get; set; }
    }
}