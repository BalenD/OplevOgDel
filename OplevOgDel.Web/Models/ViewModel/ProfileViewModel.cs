using Microsoft.AspNetCore.Mvc.Rendering;
using OplevOgDel.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Models.ViewModel
{
    public class ProfileViewModel
    {
        public ProfileDto Profile { get; set; }
        public ListOfExpsDto SelectedListOfExps { get; set; }
        public SelectList ListOfListOfExps { get; set; }
    }
}
