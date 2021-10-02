using Billdeer.Core.Entities.Abstract;
using Billdeer.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Entities.Concrete
{
    public class AdvertPackage : EntityBase
    {
        public long AdvertId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DeliveryTime { get; set; }
        public int Revision { get; set; }

    }
}
