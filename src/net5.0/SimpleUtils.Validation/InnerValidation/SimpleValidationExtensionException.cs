using System;
using System.Runtime.Serialization;

namespace SimpleUtils.Validation.InnerValidation
{
    internal sealed class SimpleValidationExtensionException : ArgumentException
    {
        internal SimpleValidationExtensionException(string itemName, string message)
            : base(itemName, message)
        {
        }

        private SimpleValidationExtensionException(SerializationInfo info, StreamingContext context) :
             base(info, context)
        {
        }
    }
}
