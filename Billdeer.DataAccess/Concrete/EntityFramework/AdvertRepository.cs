using Billdeer.Core.DataAccess.EntityFreamwork;
using Billdeer.DataAccess.Abstract;
using Billdeer.DataAccess.Concrete.EntityFramework.Contexts;
using Billdeer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.DataAccess.Concrete.EntityFramework
{
    public class AdvertRepository : EfEntityRepositoryBase<Advert, BilldeerDbContext>, IAdvertRepository
    {
        public AdvertRepository(BilldeerDbContext context) : base(context)
        {

        }

    }
}
