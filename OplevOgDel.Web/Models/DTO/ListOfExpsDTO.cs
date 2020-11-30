using System;
using System.Collections.Generic;

namespace OplevOgDel.Web.Models.Dto
{
    public class ListOfExpsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ProfileId { get; set; }
        public List<ListOfExpsExperienceDto> ListOfExpsExperiences { get; set; } = new List<ListOfExpsExperienceDto>();
    }
}
