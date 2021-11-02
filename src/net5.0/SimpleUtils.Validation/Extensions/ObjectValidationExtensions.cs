using System.Runtime.CompilerServices;
using SimpleUtils.Validation.Common;

namespace SimpleUtils.Validation.Extensions
{
    public static class ObjectValidationExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IValidatable<TItem> NotNull<TItem>(this IValidatable<TItem> itemValidator)
        {
            const string itemIsNull = "Item is null";

            if (itemValidator.Item is null)
            {
                itemValidator.FailValidation(itemIsNull);
            }
            return itemValidator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IValidatableProperty<TItem, TProperty> NotNull<TItem, TProperty>(this IValidatableProperty<TItem, TProperty> itemValidator)
        {
            NotNull((IValidatable<TProperty>)itemValidator);
            return itemValidator;
        }
    }
}
