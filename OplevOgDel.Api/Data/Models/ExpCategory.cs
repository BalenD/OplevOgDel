using OplevOgDel.Api.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Data.Models
{
    public class ExpCategory : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
