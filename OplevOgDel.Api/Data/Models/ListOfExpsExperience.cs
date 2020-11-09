using OplevOgDel.Api.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Data.Models
{
    public class ListOfExpsExperience : IEntity
    {
        public Guid Id { get; set; }
        public Guid ListOfExpsId { get; set; }
        public ListOfExps ListOfExps { get; set; }
        public Guid ExperienceId { get; set; }
        public Experience Experience { get; set; }
    }
}
