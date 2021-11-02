using SimpleUtils.Validation.Common.Exceptions;
using SimpleUtils.Validation.Extensions;
using SimpleUtils.Validation.UnitTests.Models;
using Xunit;

namespace SimpleUtils.Validation.UnitTests.ValueTypeExtensions
{
    public sealed class NotDefaultTest
    {
        [Fact]
        public void NotDefaultValue_SuccessfullyEnds()
        {
            const TestEnum testEnum = TestEnum.NotDefaultValue;

            Assert.Equal(testEnum, testEnum.AsValidatable(nameof(testEnum)).NotDefault().Item);
        }

        [Fact]
        public void DefaultValue_Throws_SimpleValidationException()
        {
            const TestEnum testEnum = default;

            Assert.ThrowsAny<SimpleValidationException>(
                () => testEnum.AsValidatable(nameof(testEnum)).NotDefault());
        }

        [Fact]
        public void Property_NotDefaultValue_SuccessfullyEnds()
        {
            var testItem = new TestItem
            {
                EnumProperty = TestEnum.NotDefaultValue
            };

            Assert.Equal(
                testItem.EnumProperty,
                testItem.AsValidatable(nameof(testItem))
                    .ValidateProperty(item => (item.EnumProperty, nameof(item.EnumProperty)))
                    .NotDefault().Item);
        }

        [Fact]
        public void Property_DefaultValue_Throws_SimpleValidationException()
        {
            var testItem = new TestItem();

            Assert.ThrowsAny<SimpleValidationException>(
                () => testItem.AsValidatable(nameof(testItem))
                    .ValidateProperty(item => (item.EnumProperty, nameof(item.EnumProperty)))
                    .NotDefault());
        }
    }
}
