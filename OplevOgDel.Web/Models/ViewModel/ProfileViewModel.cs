using Microsoft.AspNetCore.Mvc.Rendering;
using OplevOgDel.Web.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Models.ViewModel
{
    public class ProfileViewModel
    {
        public ProfileDTO Profile { get; set; }
        public ListOfExpsDTO SelectedListOfExps { get; set; }
        public SelectList ListOfListOfExps { get; set; }
    }
}
