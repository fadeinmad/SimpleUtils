using SimpleUtils.Validation.Common;
using SimpleUtils.Validation.Extensions;
using SimpleUtils.Validation.UnitTests.Models;
using Xunit;

namespace SimpleUtils.Validation.UnitTests.ValidationExtensions
{
    public sealed class EndPropertyTest
    {
        [Fact]
        public void Returns_IValidatableItem()
        {
            var testItem = new TestItem();
            const string itemName = nameof(testItem);

            var validator = testItem.AsValidatable(itemName)
                .ValidateProperty(item => (item.Property, nameof(item.Property)))
                .EndProperty();

            Assert.NotNull(validator);
            Assert.IsType<ValidatableItem<TestItem>>(validator);
            Assert.IsAssignableFrom<IValidatable<TestItem>>(validator);
            Assert.Equal(validator.ItemName, itemName);
            Assert.Equal(validator.Item, testItem);
            Assert.Equal(validator.ToString(), itemName);
        }
    }
}
