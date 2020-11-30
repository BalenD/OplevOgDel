using OplevOgDel.Api.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace OplevOgDel.Api.Models.Dto.ExperienceDto
{
    public class EditExperienceDto
    {
        [StringLength(maximumLength: 150, MinimumLength = 5, ErrorMessage = "Name must be Less than 150 characters and more than 5 characters")]
        public string Name { get; set; }
        public string Description { get; set; }
        [StringLength(maximumLength: 250, MinimumLength = 5, ErrorMessage = "City must be Less than 250 characters and more than 5 characters")]
        public string City { get; set; }
        [StringLength(maximumLength: 500, MinimumLength = 5, ErrorMessage = "Address must be Less than 500 characters and more than 5 characters")]
        public string Address { get; set; }
        public Category Category { get; set; }
    }
}
