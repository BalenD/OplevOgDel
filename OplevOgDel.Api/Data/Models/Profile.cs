using OplevOgDel.Api.Data.Base;
using OplevOgDel.Api.Data.Models.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OplevOgDel.Api.Data.Models
{
    [Table("Profiles")]
    public class Profile : BaseModel
    {
        [Required]
        [StringLength(maximumLength:150, MinimumLength = 5, ErrorMessage = "FirstName Must be Less than 150 characters and more than 5 characters")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 5, ErrorMessage = "LastName Must be Less than 150 characters and more than 5 characters")]
        public string LastName { get; set; }
        public Gender Gender { get; set; }

        //[Column(TypeName = "Date")]
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
        [Required]
        [StringLength(maximumLength: 250, MinimumLength = 5, ErrorMessage = "City Must be Less than 250 characters and more than 5 characters")]
        public string City { get; set; }
        //[Required]
        //[StringLength(maximumLength: 500, MinimumLength = 5, ErrorMessage = "Address Must be Less than 500 characters and more than 5 characters")]
        //public string Address { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<ListOfExps> ListOfExps { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ReviewReport> ReviewReports { get; set; }
        public ICollection<ExperienceReport> ExperienceReports { get; set; }
        public ICollection<Picture> Pictures { get; set; }
    }
}
