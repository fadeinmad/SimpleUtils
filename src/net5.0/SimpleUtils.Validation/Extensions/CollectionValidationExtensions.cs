using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using SimpleUtils.Validation.Common;

namespace SimpleUtils.Validation.Extensions
{
    public static class CollectionValidationExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IValidatable<IEnumerable<TItem>> NotNullOrEmpty<TItem>(this IValidatable<IEnumerable<TItem>> collectionValidator)
        {
            const string collectionIsNullOrEmpty = "Collection is null or empty";

            if (collectionValidator.Item == null || !collectionValidator.Item.Any())
            {
                collectionValidator.FailValidation(collectionIsNullOrEmpty);
            }
            return collectionValidator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IValidatableProperty<TItem, IEnumerable<TProperty>> NotNullOrEmpty<TItem, TProperty>(this IValidatableProperty<TItem, IEnumerable<TProperty>> collectionValidator)
        {
            NotNullOrEmpty((IValidatable<IEnumerable<TProperty>>)collectionValidator);
            return collectionValidator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IValidatable<IEnumerable<TItem>> ValidateElements<TItem>(
            this IValidatable<IEnumerable<TItem>> collectionValidator,
            Action<IValidatable<TItem>> validateItemAction)
        {
            validateItemAction.AsInnerValidatable(nameof(validateItemAction))
                .NotNull();

            foreach(var item in collectionValidator.Item)
            {
                validateItemAction.Invoke(item.AsValidatable($"{collectionValidator.ItemName}.<item>"));
            }

            return collectionValidator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IValidatableProperty<TItem, IEnumerable<TProperty>> ValidateElements<TItem, TProperty>(
            this IValidatableProperty<TItem, IEnumerable<TProperty>> collectionValidator,
            Action<IValidatable<TProperty>> validateItemAction)
        {
            ValidateElements((IValidatable<IEnumerable<TProperty>>)collectionValidator, validateItemAction);
            return collectionValidator;
        }
    }
}
