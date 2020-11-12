using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Models.DTO
{
    public class ProfileDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<ListOfExpsDTO> ListOfExps { get; set; } = new List<ListOfExpsDTO>();
    }
}
