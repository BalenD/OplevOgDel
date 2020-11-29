using System;
using System.ComponentModel.DataAnnotations;

namespace OplevOgDel.Api.Models.Dto.ProfileDto
{
    public class EditProfileDto
    {
        [StringLength(maximumLength: 150, MinimumLength = 5, ErrorMessage = "FirstName Must be Less than 150 characters and more than 5 characters")]
        public string FirstName { get; set; }
        [StringLength(maximumLength: 150, MinimumLength = 5, ErrorMessage = "LastName Must be Less than 150 characters and more than 5 characters")]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        [StringLength(maximumLength: 250, MinimumLength = 5, ErrorMessage = "City Must be Less than 250 characters and more than 5 characters")]
        public string City { get; set; }
        [StringLength(maximumLength: 500, MinimumLength = 5, ErrorMessage = "Address Must be Less than 500 characters and more than 5 characters")]
        public string Address { get; set; }
    }
}
