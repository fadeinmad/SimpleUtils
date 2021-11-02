using SimpleUtils.Validation.Common;
using SimpleUtils.Validation.Extensions;
using SimpleUtils.Validation.InnerValidation;
using SimpleUtils.Validation.UnitTests.Models;
using Xunit;

namespace SimpleUtils.Validation.UnitTests.ValidationExtensions
{
    public sealed class AsValidatableTest
    {
        [Fact]
        public void ValidItemName_Returns_IValidatable()
        {
            var testItem = new TestItem();
            const string itemName = nameof(testItem);

            var validator = testItem.AsValidatable(itemName);

            Assert.NotNull(validator);
            Assert.IsType<ValidatableItem<TestItem>>(validator);
            Assert.IsAssignableFrom<IValidatable<TestItem>>(validator);
            Assert.Equal(validator.ItemName, itemName);
            Assert.Equal(validator.Item, testItem);
            Assert.Equal(validator.ToString(), itemName);
        }

        [Fact]
        public void InvalidItemName_Throws_ArgumentNullException()
        {
            Assert.Throws<SimpleValidationExtensionException>(
                () => new TestItem().AsValidatable(null));
        }
    }
}