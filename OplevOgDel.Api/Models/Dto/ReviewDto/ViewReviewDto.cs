using System;

namespace OplevOgDel.Api.Models.Dto.ReviewDto
{
    public class ViewReviewDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid ProfileId { get; set; }
    }
}
