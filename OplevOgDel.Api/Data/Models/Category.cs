using OplevOgDel.Api.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Data.Models
{
    [Table("Categories")]
    public class Category : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
