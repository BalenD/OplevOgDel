using System;

namespace OplevOgDel.Api.Data.Base
{
    interface IEditableEntity
    {
        DateTimeOffset CreatedOn { get; set; }
        DateTimeOffset? ModifiedOn { get; set; }
    }
}
