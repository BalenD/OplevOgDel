using OplevOgDel.Web.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Models.ViewModel
{
    public class ReportViewModel
    {
        public List<ExperienceReportDTO> ExperienceReports { get; set; }
        public List<ReviewReportDTO> ReviewReports { get; set; }
    }
}
