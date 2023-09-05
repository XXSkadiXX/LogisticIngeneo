using System.Diagnostics.CodeAnalysis;

namespace Logistic.Common.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class HttpResponseException : Exception
    {
        public int Status { get; set; }
        public object Value { get; set; } = null!;
    }
}
