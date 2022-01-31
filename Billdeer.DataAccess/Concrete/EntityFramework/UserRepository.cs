using Billdeer.Core.DataAccess.EntityFreamwork;
using Billdeer.Core.Entities.Concrete;
using Billdeer.DataAccess.Abstract;
using Billdeer.DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billdeer.DataAccess.Concrete.EntityFramework
{
    public class UserRepository : EfEntityRepositoryBase<User, BilldeerDbContext>, IUserRepository
    {
        public UserRepository(BilldeerDbContext context) : base(context)
        {

        }

        public List<OperationClaim> GetClaims(User user)
        {
            var result = from operationClaim in _context.OperationClaims
                         join userOperationClaim in _context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == user.Id
                         select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
            return result.ToList();
        }
    }
}
