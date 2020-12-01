using System;
using System.ComponentModel.DataAnnotations;

namespace OplevOgDel.Api.Models.Dto.ReviewDto
{
    public class NewReviewDto
    {
        [Required(ErrorMessage = "A review description is required")]
        [StringLength(maximumLength: 1000, MinimumLength = 10, ErrorMessage = "Description Must be Less than 1000 characters and more than 10 characters")]
        public string Description { get; set; }
        public Guid ProfileId { get; set; }
        public Guid ExperienceId { get; set; }
    }
}
