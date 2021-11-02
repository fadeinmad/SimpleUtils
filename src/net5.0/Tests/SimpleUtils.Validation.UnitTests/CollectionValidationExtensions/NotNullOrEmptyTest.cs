using System;
using System.Collections.Generic;
using SimpleUtils.Validation.Common.Exceptions;
using SimpleUtils.Validation.Extensions;
using SimpleUtils.Validation.UnitTests.Models;
using Xunit;

namespace SimpleUtils.Validation.UnitTests.CollectionValidationExtensions
{
    public sealed class NotNullOrEmptyTest
    {
        [Fact]
        public void ValidCollection_SuccessfullyEnds()
        {
            var collection = new[]
            {
                new TestItem()
            };
            const string collectionName = nameof(collection);

            var collectionValidator = collection.AsValidatable(collectionName)
                .NotNullOrEmpty();

            Assert.NotNull(collectionValidator.Item);
            Assert.NotEmpty(collectionValidator.Item);
        }

        [Fact]
        public void EmptyCollection_Throws_SimpleValidationException()
        {
            var collection = Array.Empty<TestItem>();

            Assert.ThrowsAny<SimpleValidationException>(
                () => collection.AsValidatable(nameof(collection))
                    .NotNullOrEmpty());
        }

        [Fact]
        public void NullCollection_Throws_SimpleValidationException()
        {
            var collection = (IEnumerable<TestItem>)null;

            Assert.ThrowsAny<SimpleValidationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => collection.AsValidatable(nameof(collection))
                    .NotNullOrEmpty());
        }

        [Fact]
        public void Property_ValidCollection_SuccessfullyEnds()
        {
            var itemToValidate = new TestItem
            {
                PropertyCollection = new[]
                {
                    new TestProperty()
                }
            };
            const string itemName = nameof(itemToValidate);
            const string propertyCollectionName = nameof(itemToValidate.PropertyCollection);

            var collectionValidator = itemToValidate.AsValidatable(itemName)
                .ValidateProperty(item => (item.PropertyCollection, propertyCollectionName))
                .NotNullOrEmpty();

            Assert.NotNull(collectionValidator.Item);
            Assert.NotEmpty(collectionValidator.Item);
        }

        [Fact]
        public void Property_EmptyCollection_Throws_SimpleValidationException()
        {
            var itemToValidate = new TestItem
            {
                PropertyCollection = Array.Empty<TestProperty>()
            };
            const string itemName = nameof(itemToValidate);
            const string propertyCollectionName = nameof(itemToValidate.PropertyCollection);

            Assert.ThrowsAny<SimpleValidationException>(
                () => itemToValidate.AsValidatable(itemName)
                    .ValidateProperty(item => (item.PropertyCollection, propertyCollectionName))
                    .NotNullOrEmpty());
        }

        [Fact]
        public void Property_NullCollection_Throws_SimpleValidationException()
        {
            var itemToValidate = new TestItem();
            const string itemName = nameof(itemToValidate);
            const string propertyCollectionName = nameof(itemToValidate.PropertyCollection);

            Assert.ThrowsAny<SimpleValidationException>(
                () => itemToValidate.AsValidatable(itemName)
                    .ValidateProperty(item => (item.PropertyCollection, propertyCollectionName))
                    .NotNullOrEmpty());
        }
    }
}
