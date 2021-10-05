using Billdeer.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Entities.Concrete
{
    public class Freelancer : EntityBase
    {
        public long UserId { get; set; }
        public long TotalJob { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Rank { get; set; }
    }
}
