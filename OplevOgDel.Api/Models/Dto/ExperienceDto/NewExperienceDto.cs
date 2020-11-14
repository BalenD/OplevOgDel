using System.ComponentModel.DataAnnotations;

namespace OplevOgDel.Api.Models.Dto.ExperienceDto
{
    public class NewExperienceDto
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
        [Required(ErrorMessage = "Category must not be empty")]
        [StringLength(maximumLength: 500, MinimumLength = 5, ErrorMessage = "Category must be Less than 500 characters and more than 5 characters")]
        public string Category { get; set; }
    }
}
