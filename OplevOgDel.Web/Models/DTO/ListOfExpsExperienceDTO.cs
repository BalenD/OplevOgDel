using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Models.DTO
{
    public class ListOfExpsExperienceDTO
    {
        public Guid Id { get; set; }
        public Guid ListOfExpsId { get; set; }
        public ListOfExpsDTO ListOfExps { get; set; }
        public Guid ExperienceId { get; set; }
        public ExperienceDTO Experience { get; set; }
    }
}
