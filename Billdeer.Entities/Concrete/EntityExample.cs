using Billdeer.Core.Entities.Abstract;
using Billdeer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Entities.Concrete
{
    public class EntityExample : EntityBase<int>, IEntity
    {
        public string Name { get; set; }
        public ICollection<ForeignExample> ForeignExamples { get; set; }
    }
}
