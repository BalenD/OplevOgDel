using OplevOgDel.Web.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Models.ViewModel
{
    public class HomeViewModel
    {
        public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
        public List<ExperienceDTO> Experiences { get; set; } = new List<ExperienceDTO>();
    }
}
