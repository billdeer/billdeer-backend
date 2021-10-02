using Billdeer.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Entities.Concrete
{
    public class PackageProperty : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
