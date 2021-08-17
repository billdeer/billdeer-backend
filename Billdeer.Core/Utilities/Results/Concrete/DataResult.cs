
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

        public T Data { get; }
    }
}
