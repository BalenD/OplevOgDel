using OplevOgDel.Api.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OplevOgDel.Api.Data.Models
{
    [Table("Experiences")]
    public class Experience : BaseModel
    {
        [Required(ErrorMessage = "Name must not be empty")]
        [StringLength(maximumLength: 150, MinimumLength = 5, ErrorMessage = "Name must be Less than 150 characters and more than 5 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description must not be empty")]
        public string Description { get; set; }
        [Required(ErrorMessage = "City must not be empty")]
        [StringLength(maximumLength: 250, MinimumLength = 5, ErrorMessage = "City must be Less than 250 characters and more than 5 characters")]
        public string City { get; set; }
        [Required(ErrorMessage = "Address must not be empty")]
        [StringLength(maximumLength: 500, MinimumLength = 5, ErrorMessage = "Address must be Less than 500 characters and more than 5 characters")]
        public string Address { get; set; }
        public string Pictures { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid ProfileId { get; set; }
        public Profile Creator { get; set; }
        public ICollection<ListOfExpsExperience> ListOfExpsExperiences { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ExperienceReport> ExperienceReports { get; set; }
    }
}
