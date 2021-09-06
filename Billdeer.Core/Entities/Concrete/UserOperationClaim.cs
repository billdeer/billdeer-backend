using Billdeer.Core.Entities.Abstract;
using Billdeer.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Core.Entities.Concrete
{
    public class UserOperationClaim : EntityBase<long>, IEntity
    {
        public long UserId { get; set; }
        public User User { get; set; }

        public int OperationClaimId { get; set; }
        public OperationClaim OperationClaim { get; set; }



    }
}
