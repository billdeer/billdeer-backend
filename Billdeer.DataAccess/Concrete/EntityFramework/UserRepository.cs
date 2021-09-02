using Billdeer.Core.DataAccess.EntityFreamwork;
using Billdeer.Core.Entities.Concrete;
using Billdeer.DataAccess.Abstract;
using Billdeer.DataAccess.Concrete.EntityFramework.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace Billdeer.DataAccess.Concrete.EntityFramework
{
    public class UserRepository : EfEntityRepositoryBase<User, BilldeerDbContext>, IUserRepository
    {
        public UserRepository(BilldeerDbContext context) : base(context)
        {

        }
        public List<OperationClaim> GetClaims(User user)
        {
            //var result = from operationClaim in _context.OperationClaims
            //             join u in _context.Users
            //                 on operationClaim.Id equals u.OperationClaims
            //             where userOperationClaim.UserId == user.Id
            //             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
            //return result.ToList();
            return new List<OperationClaim>();
        }
    }
}
