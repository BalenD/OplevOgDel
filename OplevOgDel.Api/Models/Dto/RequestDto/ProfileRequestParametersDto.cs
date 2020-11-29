namespace OplevOgDel.Api.Models.Dto.RequestDto
{
    public class ProfileRequestParametersDto : BaseRequestParametersDto
    {
        public string SearchByFirstName { get; set; }
        public string SearchByLastName { get; set; }
    }
}
