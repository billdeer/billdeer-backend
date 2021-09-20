using Billdeer.Core.Entities.Abstract;
using Billdeer.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Billdeer.Core.Entities.Concrete
{
    public class OperationClaim : EntityBase<int>, IEntity
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }

    }
}
