using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Core.Utilities.Results.Concrete
{
    public class ApiDataResult<T> : ApiResult
    {
        public T Data { get; set; }
    }
}
