namespace OplevOgDel.Api.Models.Dto.RequestDto
{
    public class ExperienceRequestParametersDto : BaseRequestParametersDto
    {
        /// <summary>
        /// Filter by city name
        /// </summary>
        /// <example>København</example>
        public string FilterByCity { get; set; }
        /// <summary>
        /// Filter by category
        /// </summary>
        /// <example>Musik</example>
        public string FilterByCategory { get; set; }
        /// <summary>
        /// Search by name of the experience
        /// </summary>
        /// <example>City Cafe</example>
        public string SearchString { get; set; }
    }
}
