using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Models.DTO
{
    public class ReviewReportDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid ProfileId { get; set; }
        public ProfileDTO Creator { get; set; }
        public Guid ExperienceId { get; set; }
        public ReviewDTO Review { get; set; }
    }
}
