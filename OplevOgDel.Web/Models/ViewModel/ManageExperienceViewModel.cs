using OplevOgDel.Web.Models.Dto;
using System;
using System.Collections.Generic;

namespace OplevOgDel.Web.Models.ViewModel
{
    public class ManageExperienceViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryDto Category { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public List<ExperienceReportDto> ExperienceReports { get; set; }
        public List<ReviewDto> Reviews { get; set; }
    }
}
