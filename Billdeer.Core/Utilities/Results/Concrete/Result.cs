
using Billdeer.Core.Utilities.Results.ComplexTypes;

namespace Billdeer.Core.Utilities.Results
{
    public class Result : IResult
    {

        public ResultStatus ResultStatus { get; }
        public string Message { get; }

        public Result(ResultStatus resultStatus, string message) : this(resultStatus)
        {
            Message = message;
        }

        public Result(ResultStatus resultStatus)
        {
            ResultStatus = resultStatus;
        }
    }
}
