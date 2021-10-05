using Billdeer.Core.DataAccess;
using Billdeer.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Core.Utilities.ToolKit
{
    public class CheckEntities<TRepository, T>
        where T : class, IEntity, new()
        where TRepository : IEntityRepository<T>
    {
        public static bool Exist(TRepository repository, long Id)
        {
            var result = repository.Queryable(x => x.Id == Id);
            return result.Any();
        }
    }
}
