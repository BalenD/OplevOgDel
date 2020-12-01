using OplevOgDel.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Models.ViewModel
{
    public class ReportsViewModel
    {
        public List<ExperienceDto> Experiences { get; set; } = new List<ExperienceDto>();
    }
}
