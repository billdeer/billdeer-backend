
using Billdeer.Core.Utilities.Results.ComplexTypes;
using System.Collections.Generic;
using System.Net;

namespace Billdeer.Core.Utilities.Results
{
    public class ApiResult
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }
        public string InternalMessage { get; set; }
        public List<string> Errors { get; set; }
        public string URL { get; set; }
        public object Data { get; set; }
    }
}
