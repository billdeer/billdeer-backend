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
    public class AdvertPackageRepository : EfEntityRepositoryBase<AdvertPackage, BilldeerDbContext>, IAdvertPackageRepository
    {
        public AdvertPackageRepository(BilldeerDbContext context) : base(context)
        {

        }

        //:. Some joining must be add.
    }
}
