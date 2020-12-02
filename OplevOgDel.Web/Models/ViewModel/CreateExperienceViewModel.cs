using Microsoft.AspNetCore.Mvc.Rendering;
using OplevOgDel.Web.Models.Dto;
using System.Collections.Generic;

namespace OplevOgDel.Web.Models.ViewModel
{
    public class CreateExperienceViewModel
    {
        public CreateExperienceDto Experience { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
