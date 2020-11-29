using OplevOgDel.Web.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Models.ViewModel
{
    public class ManageExperienceViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryDTO Category { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public List<ExperienceReportDTO> ExperienceReports { get; set; }
        public List<NewReviewDTO> Reviews { get; set; }
    }
}
