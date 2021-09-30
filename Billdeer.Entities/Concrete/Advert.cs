using Billdeer.Core.Entities.Abstract;
using Billdeer.Core.Entities.Common;
using Billdeer.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Entities.Concrete
{
    public class Advert : EntityBase
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
