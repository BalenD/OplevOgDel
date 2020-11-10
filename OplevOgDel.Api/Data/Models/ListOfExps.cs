using OplevOgDel.Api.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OplevOgDel.Api.Data.Models
{
    [Table("ListOfExperiences")]
    public class ListOfExps : BaseModel
    {
        [Required(ErrorMessage = "Name must not be empty")]
        [StringLength(maximumLength: 250, MinimumLength = 5, ErrorMessage = "Name of the list must be Less than 250 characters and more than 5 characters")]
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ProfileId { get; set; }
        public Profile Creator { get; set; }
        public ICollection<ListOfExpsExperience> ListOfExpsExperiences { get; set; }

    }
}
