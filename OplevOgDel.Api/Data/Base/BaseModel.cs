using System;
using System.ComponentModel.DataAnnotations;

namespace OplevOgDel.Api.Data.Base
{
    public class BaseModel : IEntity, IEditableEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
