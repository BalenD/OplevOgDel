using System;

namespace OplevOgDel.Api.Data.Base
{
    interface IDeletableEntity
    {
        bool IsDeleted { get; set; }
        DateTimeOffset? DeletedOn { get; set; }
    }
}
