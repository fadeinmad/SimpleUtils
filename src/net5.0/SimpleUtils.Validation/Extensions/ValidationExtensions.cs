using System;
using System.Runtime.CompilerServices;
using SimpleUtils.Validation.Common;
using SimpleUtils.Validation.Common.Exceptions;
using SimpleUtils.Validation.InnerValidation;

namespace SimpleUtils.Validation.Extensions
{
    public static class ValidationExtensions
    {
        public static IValidatable<TItem> AsValidatable<TItem>(this TItem item, string itemName)
        {
            if(string.IsNullOrWhiteSpace(itemName))
            {
                throw new SimpleValidationExtensionException(
                    nameof(itemName),
                    "ValidationExtensions.AsValidatable argument is empty");
            }

            return new ValidatableItem<TItem>
            {
                Item = item,
                ItemName = itemName
            };
        }

        public static IValidatableProperty<TItem, TProperty> ValidateProperty<TItem, TProperty>(
            this IValidatable<TItem> itemValidator,
            Func<TItem, (TProperty, string)> propertySelector)
        {
            propertySelector.AsInnerValidatable(nameof(propertySelector)).NotNull();

            var (property, propertyName) = propertySelector.Invoke(itemValidator.Item);

            if(string.IsNullOrWhiteSpace(propertyName))
            {
                throw new SimpleValidationExtensionException(
                    nameof(propertySelector),
                    $"{nameof(propertySelector)} returns the empty property name");
            }

            return new ValidatableProperty<TItem, TProperty>(itemValidator)
            {
                Item = property,
                ItemName = propertyName
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IValidatable<TItem> ValidateProperty<TItem, TProperty>(
            this IValidatable<TItem> itemValidator,
            Func<TItem, (TProperty, string)> propertySelector,
            Action<IValidatable<TProperty>> validationAction)
        {
            propertySelector.AsInnerValidatable(nameof(propertySelector)).NotNull();
            validationAction.AsInnerValidatable(nameof(validationAction)).NotNull();

            var (property, propertyName) = propertySelector.Invoke(itemValidator.Item);

            propertyName.AsInnerValidatable($"{nameof(propertySelector)} for {itemValidator.ItemName}")
                .NotNullOrWhiteSpace();

            validationAction.Invoke(property.AsValidatable($"{itemValidator.ItemName}.{propertyName}"));

            return itemValidator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ValidateCustom<TItem>(
            this IValidatable<TItem> itemValidator,
            Action<IValidatable<TItem>> customValidation)
        {
            customValidation.AsInnerValidatable(nameof(customValidation)).NotNull();
            try
            {
                customValidation.Invoke(itemValidator);
            }
            catch (SimpleValidationExtensionException)
            {
                throw;
            }
            catch (SimpleValidationException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new ValidateCustomUnexpectedException<TItem>(
                    itemValidator,
                    "Unexpected custom exception was thrown",
                    exception);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FailValidation<TItem>(
            this IValidatable<TItem> validator,
            string message)
        {
            if(string.IsNullOrWhiteSpace(message))
            {
                throw new SimpleValidationExtensionException(
                    nameof(message),
                    "ValidationExtensions.FailValidation argument is empty");
            }

            if (validator is InnerValidatableItem<TItem>)
            {
                throw new SimpleValidationExtensionException(validator.ItemName, message);
            }

            throw new SimpleValidationException<TItem>(validator, message);
        }

        internal static IValidatable<TItem> AsInnerValidatable<TItem>(this TItem item, string itemName)
        {
            if (string.IsNullOrWhiteSpace(itemName))
            {
                throw new ArgumentNullException(itemName);
            }

            return new InnerValidatableItem<TItem>
            {
                Item = item,
                ItemName = itemName
            };
        }
    }
}
