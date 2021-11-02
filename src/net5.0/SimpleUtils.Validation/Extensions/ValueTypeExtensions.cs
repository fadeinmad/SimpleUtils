using System.Runtime.CompilerServices;
using SimpleUtils.Validation.Common;

namespace SimpleUtils.Validation.Extensions
{
    public static class ValueTypeExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IValidatable<TItem> NotDefault<TItem>(this IValidatable<TItem> itemValidator)
            where TItem : struct
        {
            const string itemHasDefaultValue = "Item has default value";

            if (default(TItem).Equals(itemValidator.Item))
            {
                itemValidator.FailValidation(itemHasDefaultValue);
            }
            return itemValidator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IValidatableProperty<TItem, TProperty> NotDefault<TItem, TProperty>(
            this IValidatableProperty<TItem, TProperty> itemValidator)
            where TProperty : struct
        {
            NotDefault((IValidatable<TProperty>)itemValidator);
            return itemValidator;
        }
    }
}
