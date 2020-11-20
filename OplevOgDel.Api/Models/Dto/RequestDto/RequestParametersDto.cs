namespace OplevOgDel.Api.Models.Dto.RequestDto
{
    public class RequestParametersDto
    {
        public string SortByCity { get; set; }
        public string SortByCategory { get; set; }
        public string SearchString { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
