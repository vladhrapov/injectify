using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

[assembly: InternalsVisibleTo("Injectify.Autofac")]
[assembly: InternalsVisibleTo("Injectify.Microsoft.DependencyInjection")]

namespace Injectify.Exceptions
{
    [Serializable]
    internal class InjectifyException : Exception
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
