using System;

namespace OplevOgDel.Web.Models.Dto
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Clicked { get; set; }
    }
}
