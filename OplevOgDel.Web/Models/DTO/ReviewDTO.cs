using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Models.DTO
{
    public class ReviewDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid CreatorId { get; set; }
        public string CreatorName { get; set; }
    }
}
