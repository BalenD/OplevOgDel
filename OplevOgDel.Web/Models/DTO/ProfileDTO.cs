using System.Collections.Generic;

namespace OplevOgDel.Web.Models.Dto
{
    public class ProfileDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public List<ListOfExpsDto> ListOfExps { get; set; } = new List<ListOfExpsDto>();
    }
}