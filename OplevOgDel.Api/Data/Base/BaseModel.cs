using System;
using System.ComponentModel.DataAnnotations;

namespace OplevOgDel.Api.Data.Base
{
    public class BaseModel : IEntity, IDeletableEntity, IEditableEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
    }
}
