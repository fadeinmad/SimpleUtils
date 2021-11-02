using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SimpleUtils.Validation.Common;

namespace SimpleUtils.Validation.Extensions
{
    public static class AsyncValidationExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IValidatable<TItem> ValidateProperty<TItem, TProperty>(
            this IValidatable<TItem> itemValidator,
            Func<TItem, (TProperty, string)> propertySelector,
            Func<IValidatable<TProperty>, Task> validateAsync)
        {
            propertySelector.AsInnerValidatable(nameof(propertySelector)).NotNull();
            validateAsync.AsInnerValidatable(nameof(validateAsync)).NotNull();

            var (property, propertyName) = propertySelector.Invoke(itemValidator.Item);

            propertyName.AsInnerValidatable($"{nameof(propertySelector)} for {itemValidator.ItemName}")
                .NotNullOrWhiteSpace();

            validateAsync
                .Invoke(property.AsValidatable($"{itemValidator.ItemName}.{propertyName}"))
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            return itemValidator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task ValidateCustomAsync<TItem>(
            this IValidatable<TItem> itemValidator,
            Func<IValidatable<TItem>, Task> validateAsync)
        {
            validateAsync.AsInnerValidatable(nameof(validateAsync))
                .NotNull();

            return validateAsync.Invoke(itemValidator);
        }
    }
}
