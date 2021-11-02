using SimpleUtils.Validation.Common.Exceptions;
using SimpleUtils.Validation.Extensions;
using SimpleUtils.Validation.InnerValidation;
using SimpleUtils.Validation.UnitTests.Models;
using Xunit;

namespace SimpleUtils.Validation.UnitTests.CollectionValidationExtensions
{
    public sealed class ValidateElementsTest
    {
        [Fact]
        public void ValidCollectionItems_SuccessfullyEnds()
        {
            var collection = new[]
            {
                new TestItem(),
                new TestItem()
            };
            const string collectionName = nameof(collection);

            var collectionValidator = collection.AsValidatable(collectionName)
                .ValidateElements(itemValidator => itemValidator.NotNull());

            Assert.All(collectionValidator.Item, Assert.NotNull);
        }

        [Fact]
        public void NullValidatorAction_Throws_SimpleValidationExtensionException()
        {
            var collection = new[]
            {
                new TestItem(),
                null
            };

            Assert.ThrowsAny<SimpleValidationExtensionException>(
                () => collection.AsValidatable(nameof(collection))
                    .ValidateElements(null));
        }

        [Fact]
        public void InvalidCollectionItem_Throws_SimpleValidationException()
        {
            var collection = new[]
            {
                new TestItem(),
                null
            };

            Assert.ThrowsAny<SimpleValidationException>(
                () => collection.AsValidatable(nameof(collection))
                    .ValidateElements(itemValidator => itemValidator.NotNull()));
        }

        [Fact]
        public void Property_ValidCollection_SuccessfullyEnds()
        {
            var itemToValidate = new TestItem
            {
                PropertyCollection = new[]
                {
                    new TestProperty(),
                    new TestProperty()
                }
            };

            var collectionValidator = itemToValidate.AsValidatable(nameof(itemToValidate))
                .ValidateProperty(item => (item.PropertyCollection, nameof(itemToValidate.PropertyCollection)))
                .ValidateElements(validator => validator.NotNull());

            Assert.All(collectionValidator.Item, Assert.NotNull);
        }

        [Fact]
        public void Property_NullValidatorAction_Throws_SimpleValidationExtensionException()
        {
            var itemToValidate = new TestItem
            {
                PropertyCollection = new[]
                {
                    new TestProperty(),
                    null
                }
            };

            Assert.ThrowsAny<SimpleValidationExtensionException>(
                () => itemToValidate.AsValidatable(nameof(itemToValidate))
                    .ValidateProperty(item => (item.PropertyCollection, nameof(item.PropertyCollection)))
                    .ValidateElements(null));
        }

        [Fact]
        public void Property_EmptyCollection_Throws_SimpleValidationException()
        {
            var itemToValidate = new TestItem
            {
                PropertyCollection = new[]
                {
                    new TestProperty(),
                    null
                }
            };

            Assert.ThrowsAny<SimpleValidationException>(
                () => itemToValidate.AsValidatable(nameof(itemToValidate))
                    .ValidateProperty(item => (item.PropertyCollection, nameof(itemToValidate.PropertyCollection)))
                    .ValidateElements(validator => validator.NotNull()));
        }
    }
}
