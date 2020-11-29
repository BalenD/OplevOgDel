namespace OplevOgDel.Api.Models.Dto.RequestDto
{
    public class BaseRequestParametersDto
    {
        /// <summary>
        /// The page to get
        /// </summary>
        /// <example>4</example>
        public int Page { get; set; } = 1;
        /// <summary>
        /// How many items per page
        /// </summary>
        /// <example>6</example>
        public int PageSize { get; set; } = 10;
    }
}
