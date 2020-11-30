using System;

namespace OplevOgDel.Web.Models.Dto
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        //public Guid CreatorId { get; set; }
        public Guid ProfileId { get; set; }
        public string CreatorName { get; set; }
    }
}