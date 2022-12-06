using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

[assembly: InternalsVisibleTo("Injectify.Autofac")]
[assembly: InternalsVisibleTo("Injectify.Microsoft.DependencyInjection")]

namespace Injectify.Exceptions
{
    /// <summary>
    /// Injectify common exception, throws as a generic indicator
    /// on internal issues or wrong usage.
    /// </summary>
    [Serializable]
    internal class InjectifyException : Exception
    {
        /// <summary>
        /// Public ctor without parameters.
        /// </summary>
        public InjectifyException()
        {
        }

        /// <summary>
        /// Public ctor with exception message.
        /// </summary>
        /// <param name="message">Exception message details.</param>
        public InjectifyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Public ctor with exception message and inner exception.
        /// </summary>
        /// <param name="message">Exception message details.</param>
        /// <param name="innerException">Inner exception instance.</param>
        public InjectifyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected InjectifyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
