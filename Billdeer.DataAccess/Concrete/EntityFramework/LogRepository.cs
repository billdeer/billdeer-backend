using Billdeer.Core.DataAccess.EntityFreamwork;
using Billdeer.Core.Entities.Concrete;
using Billdeer.DataAccess.Abstract;
using Billdeer.DataAccess.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.DataAccess.Concrete.EntityFramework
{
    public class LogRepository : EfEntityRepositoryBase<Log, BilldeerDbContext>, ILogRepository
    {
        public LogRepository(BilldeerDbContext context) : base(context)
        {
        }
    }
}
