using Billdeer.Core.Entities.Abstract;
using System;

namespace Billdeer.Core.Entities.Common
{
    public abstract class EntityBase : IEntity
    {
        public virtual long Id { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual DateTime? DeletedDate { get; set; }
    }
}
