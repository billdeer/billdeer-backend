using Billdeer.Core.DataAccess;
using Billdeer.Core.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Billdeer.DataAccess.Abstract
{
    public interface IUserRepository : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
    }
}
