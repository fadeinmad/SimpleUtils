using System;
using System.Runtime.Serialization;

namespace SimpleUtils.Validation.Common.Exceptions
{
    [Serializable]
    internal sealed class ValidateCustomUnexpectedException<TItem> : SimpleValidationException
    {
        internal ValidateCustomUnexpectedException(IValidatable<TItem> validator, string message, Exception customException)
            : base(validator.ToString(), message, customException)
        {
        }

        private ValidateCustomUnexpectedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
    }
}
