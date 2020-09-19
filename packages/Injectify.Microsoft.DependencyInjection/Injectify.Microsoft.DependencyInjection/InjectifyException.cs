using System;
using System.Runtime.Serialization;

namespace Injectify.Microsoft.DependencyInjection
{
    [Serializable]
    public class InjectifyException : Exception
    {
        public InjectifyException()
        {
        }

        public InjectifyException(string message) : base(message)
        {
        }

        public InjectifyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InjectifyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}