using OplevOgDel.Api.Data.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OplevOgDel.Api.Data.Models
{
    [Table("Categories")]
    public class Category : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
