using OplevOgDel.Web.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Models.ViewModel
{
    public class ManageExperienceViewModel
    {
        public ManageExperienceDTO Experience { get; set; }
        public List<ExperienceReportDTO> ExperienceReports { get; set; } = new List<ExperienceReportDTO>();
        public List<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();
        public List<ReviewReportDTO> ReviewReports { get; set; } = new List<ReviewReportDTO>();
    }
}
