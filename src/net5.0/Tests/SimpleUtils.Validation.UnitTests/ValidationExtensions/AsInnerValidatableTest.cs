using System;
using SimpleUtils.Validation.Extensions;
using SimpleUtils.Validation.InnerValidation;
using SimpleUtils.Validation.UnitTests.Models;
using Xunit;

namespace SimpleUtils.Validation.UnitTests.ValidationExtensions
{
    public sealed class AsInnerValidatableTest
    {
        [Fact]
        public void ValidItemName_Returns_IValidatable()
        {
            var testItem = new TestItem();
            const string itemName = nameof(testItem);

            var validator = testItem.AsInnerValidatable(itemName);

            Assert.NotNull(validator);
            Assert.IsType<InnerValidatableItem<TestItem>>(validator);
            Assert.Equal(validator.ItemName, itemName);
            Assert.Equal(validator.Item, testItem);
        }

        [Fact]
        public void InvalidItemName_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => new TestItem().AsInnerValidatable(null));
        }
    }
}
