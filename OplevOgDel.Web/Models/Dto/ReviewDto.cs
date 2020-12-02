using System;
using System.Collections.Generic;

namespace OplevOgDel.Web.Models.Dto
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid ProfileId { get; set; }
        public ProfileDto Profile { get; set; }
        public List<ReviewReportDto> ReviewReports { get; set; }
    }
}
