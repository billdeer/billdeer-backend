
using Billdeer.Core.Utilities.Results.ComplexTypes;

namespace Billdeer.Core.Utilities.Results
{
    public interface IResult
    {
        ResultStatus ResultStatus { get; }
        string Message { get; }
    }
}
