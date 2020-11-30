using OplevOgDel.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Models.ViewModel
{
    public class HomeViewModel
    {
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public List<ExperienceDto> Experiences { get; set; } = new List<ExperienceDto>();
    }
}
