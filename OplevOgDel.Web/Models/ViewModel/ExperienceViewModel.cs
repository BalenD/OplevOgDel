using OplevOgDel.Web.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Models.ViewModel
{
    public class ExperienceViewModel
    {
        public ExperienceDTO Experience { get; set; }
        public List<RatingDTO> Ratings { get; set; }
        public List<ReviewDTO> Reviews { get; set; }
        public List<ReportDTO> Reports { get; set; }
    }
}
