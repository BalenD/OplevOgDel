using Microsoft.AspNetCore.Mvc.Rendering;
using OplevOgDel.Web.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Models.ViewModel
{
    public class EditExperienceViewModel
    {
        public ExperienceDTO Experience { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
