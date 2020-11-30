using OplevOgDel.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Models.ViewModel
{
    public class ExperienceViewModel
    {
        public ExperienceDto Experience { get; set; }
        public RatingDto Rating { get; set; }
        public List<ReviewDto> Reviews { get; set; }
        public List<ReviewReportDto> Reports { get; set; }
    }
}
