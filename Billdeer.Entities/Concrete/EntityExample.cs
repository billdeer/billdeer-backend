using Billdeer.Core.Entities.Abstract;
using Billdeer.Core.Entities.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Billdeer.Entities.Concrete
{
    public class EntityExample : EntityBase
    {
        public string Name { get; set; }
        public ICollection<ForeignExample> ForeignExamples { get; set; }
    }
}
