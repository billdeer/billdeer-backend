
using Billdeer.Core.Utilities.Results.ComplexTypes;

namespace Billdeer.Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, ResultStatus resultStatus, string message) : base(resultStatus, message)
        {
            Data = data;
        }

        public DataResult(T data, ResultStatus resultStatus) : base(resultStatus)
        {
            Data = data;
        }

        public DataResult(ResultStatus resultStatus, string message) : base(resultStatus, message)
        {

        }

        public DataResult(ResultStatus resultStatus, object notFound) : base(resultStatus)
        {

        }

        public T Data { get; }
    }
}
