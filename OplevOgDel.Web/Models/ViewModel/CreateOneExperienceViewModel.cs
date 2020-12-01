using Microsoft.AspNetCore.Mvc.Rendering;
using OplevOgDel.Web.Models.Dto;
using System;
using System.Collections.Generic;

namespace OplevOgDel.Web.Models.ViewModel
{
    public class CreateOneExperienceViewModel
    {
        public CreateOneExperienceDto Experience { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
