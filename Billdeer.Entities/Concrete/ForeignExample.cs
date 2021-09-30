using Billdeer.Core.Entities.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Entities.Concrete
{
    public class ForeignExample : EntityBase
    {
        public string Name { get; set; }
        public long EntityExampleId { get; set; }
        [JsonIgnore]
        public EntityExample EntityExample { get; set; }
    }
}
