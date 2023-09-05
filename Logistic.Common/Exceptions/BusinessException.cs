using System.Diagnostics.CodeAnalysis;

namespace Logistic.Common.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class BusinessException : Exception
    {
        public BusinessException() : base()
        {

        }

        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(string message, Exception inner) : base(message, inner)
        {
        }

        protected BusinessException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }
}
