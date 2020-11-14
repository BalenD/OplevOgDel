using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Models.DTO
{
    public class ListOfExpsDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ProfileId { get; set; }
        public List<ListOfExpsExperienceDTO> ListOfExpsExperiences { get; set; } = new List<ListOfExpsExperienceDTO>();
    }
}
