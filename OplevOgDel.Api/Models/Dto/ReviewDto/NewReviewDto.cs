using System.ComponentModel.DataAnnotations;

namespace OplevOgDel.Api.Models.Dto.ReviewDto
{
    public class NewReviewDto
    {
        [Required(ErrorMessage = "A review description is required")]
        public string Description { get; set; }
    }
}
