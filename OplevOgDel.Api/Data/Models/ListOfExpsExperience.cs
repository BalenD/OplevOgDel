using OplevOgDel.Api.Data.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OplevOgDel.Api.Data.Models
{
    [Table("ListOfExpsExperiences")]
    public class ListOfExpsExperience : IEntity
    {
        public Guid Id { get; set; }
        public Guid ListOfExpsId { get; set; }
        public ListOfExps ListOfExps { get; set; }
        public Guid ExperienceId { get; set; }
        public Experience Experience { get; set; }
    }
}
