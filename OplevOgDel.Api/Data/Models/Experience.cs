using OplevOgDel.Api.Data.Base;
using OplevOgDel.Api.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OplevOgDel.Api.Data.Models
{
    [Table("Experiences")]
    public class Experience : BaseModel
    {
        [Required(ErrorMessage = "Name must not be empty")]
        [StringLength(maximumLength: 150, MinimumLength = 5, ErrorMessage = "Name must be Less than 150 characters and more than 5 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description must not be empty")]
        public string Description { get; set; }
        public Category Category { get; set; }
        [Required(ErrorMessage = "City must not be empty")]
        [StringLength(maximumLength: 250, MinimumLength = 5, ErrorMessage = "City must be Less than 250 characters and more than 5 characters")]
        public string City { get; set; }
        [Required(ErrorMessage = "Address must not be empty")]
        [StringLength(maximumLength: 500, MinimumLength = 5, ErrorMessage = "Address must be Less than 500 characters and more than 5 characters")]
        public string Address { get; set; }
        public Profile Creator { get; set; }
    }
}
