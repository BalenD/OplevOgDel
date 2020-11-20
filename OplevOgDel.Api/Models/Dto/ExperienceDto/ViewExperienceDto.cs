using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Models.Dto.PictureDto;
using System;
using System.Collections.Generic;

namespace OplevOgDel.Api.Models.Dto.ExperienceDto
{
    public class ViewExperienceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public Category Category { get; set; }
        public IEnumerable<ViewPictureDto> Pictures { get; set; }
    }
}
