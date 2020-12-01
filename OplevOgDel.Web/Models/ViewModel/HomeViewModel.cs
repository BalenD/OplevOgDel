using OplevOgDel.Web.Models.Dto;
using System.Collections.Generic;

namespace OplevOgDel.Web.Models.ViewModel
{
    public class HomeViewModel
    {
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public List<ExperienceDto> Experiences { get; set; } = new List<ExperienceDto>();
        public string[] ButtonColors { get; set; } = { "red", "green", "purple", "orange", "blue", "brown" };
        public string[] ButtonIcons { get; set; } = { "fa-utensils", "fa-tree-alt", "fa-guitar", "fa-running", "fa-globe-europe", "fa-landmark" };
    }
}
