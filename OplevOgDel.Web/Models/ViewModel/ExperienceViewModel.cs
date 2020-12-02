using OplevOgDel.Web.Models.Dto;
using System.Collections.Generic;

namespace OplevOgDel.Web.Models.ViewModel
{
    public class ExperienceViewModel
    {
        public ExperienceDto Experience { get; set; }
        public RatingDto Rating { get; set; }
        public CreateReviewDto CreateReview { get; set; }
        public List<ReviewDto> Reviews { get; set; }
        public List<ReviewReportDto> Reports { get; set; }
    }
}
