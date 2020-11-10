using OplevOgDel.Api.Data.Models;
using System;

namespace OplevOgDel.Api.Models
{
    public class ViewOneExperience
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public Guid ExpCategoryId { get; set; }
        public ExpCategory Category { get; set; }
        public Guid ProfileId { get; set; }
        public Profile Creator { get; set; }
    }
}
