using Microsoft.AspNetCore.Mvc.Rendering;
using OplevOgDel.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Models.ViewModel
{
    public class EditExperienceViewModel
    {
        public ExperienceDto Experience { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
