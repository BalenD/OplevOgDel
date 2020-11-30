using System;

namespace OplevOgDel.Web.Models.Dto
{
    public class ListOfExpsExperienceDto
    {
        public Guid Id { get; set; }
        public Guid ListOfExpsId { get; set; }
        public ListOfExpsDto ListOfExps { get; set; }
        public Guid ExperienceId { get; set; }
        public ExperienceDto Experience { get; set; }
    }
}
