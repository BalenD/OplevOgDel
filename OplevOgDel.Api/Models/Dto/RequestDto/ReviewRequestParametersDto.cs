using System;

namespace OplevOgDel.Api.Models.Dto.RequestDto
{
    public class ReviewRequestParametersDto : BaseRequestParametersDto
    {
        /// <summary>
        /// Date to filter by
        /// </summary>
        /// <example></example>
        public DateTime FilterByDate { get; set; }
        /// <summary>
        /// Owner to filter by (can be first name or last name)
        /// </summary>
        /// <example>John</example>
        public string FilterByOwner { get; set; }
        /// <summary>
        /// Search by strings in the description of the review
        /// </summary>
        /// <example>"good"</example>
        public string SearchString { get; set; }
    }
}
