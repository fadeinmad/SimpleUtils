using System;
using System.Runtime.Serialization;

namespace SimpleUtils.Validation.Common.Exceptions
{
    [Serializable]
    public abstract class SimpleValidationException : ArgumentException
    {
        protected SimpleValidationException()
        {
        }

        protected SimpleValidationException(string itemName, string message)
            : base($"Validation failed: {message}", itemName)
        {
        }

        protected SimpleValidationException(string itemName, string message, Exception innerException)
            : base($"Validation failed: {message}", itemName, innerException)
        {
        }

        protected SimpleValidationException(SerializationInfo info, StreamingContext context) :
             base(info, context)
        {
        }
    }

    [Serializable]
    internal sealed class SimpleValidationException<TItem> : SimpleValidationException
    {
        internal SimpleValidationException(IValidatable<TItem> validator, string message)
            : base(validator.ToString(), message)
        {
        }

        private SimpleValidationException(SerializationInfo info, StreamingContext context) :
             base(info, context)
        {
        }
    }
}
