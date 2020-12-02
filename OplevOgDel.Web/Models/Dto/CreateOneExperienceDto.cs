using System;

namespace OplevOgDel.Web.Models.Dto
{
    public class CreateOneExperienceDto
    {
        public Guid ProfileId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}
