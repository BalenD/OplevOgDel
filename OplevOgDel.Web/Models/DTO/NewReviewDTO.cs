using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Models.DTO
{
    public class NewReviewDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid ProfileId { get; set; }
        public ProfileDTO Profile { get; set; }
        public List<ReviewReportDTO> ReviewReports { get; set; }
    }
}
