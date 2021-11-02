using System.Runtime.CompilerServices;
using SimpleUtils.Validation.Common;

namespace SimpleUtils.Validation.Extensions
{
    public static class StringValidationExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IValidatable<string> NotNullOrWhiteSpace(this IValidatable<string> itemValidator)
        {
            const string itemIsNullOrWhiteSpace = "Item is null, empty or consist only of white-space characters";

            if (string.IsNullOrWhiteSpace(itemValidator.Item))
            {
                itemValidator.FailValidation(itemIsNullOrWhiteSpace);
            }
            return itemValidator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IValidatableProperty<TItem, string> NotNullOrWhiteSpace<TItem>(
            this IValidatableProperty<TItem, string> itemValidator)
        {
            NotNullOrWhiteSpace((IValidatable<string>)itemValidator);
            return itemValidator;
        }
    }
}
