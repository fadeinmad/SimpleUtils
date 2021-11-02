using SimpleUtils.Validation.Common.Exceptions;
using SimpleUtils.Validation.Extensions;
using SimpleUtils.Validation.UnitTests.Models;
using Xunit;

namespace SimpleUtils.Validation.UnitTests.StringValidationExtensions
{
    public sealed class NotNullOrWhiteSpace
    {
        [Fact]
        public void NotNullOrWhiteSpaceValue_SuccessfullyEnds()
        {
            const string testItem = "valid string";

            Assert.Equal(testItem, testItem.AsValidatable(nameof(testItem)).NotNullOrWhiteSpace().Item);
        }

        [Fact]
        public void EmptyValue_Throws_SimpleValidationException()
        {
            var testItem = string.Empty;

            Assert.ThrowsAny<SimpleValidationException>(
                () => testItem.AsValidatable(nameof(testItem)).NotNullOrWhiteSpace());
        }

        [Fact]
        public void WhiteSpacesValue_Throws_SimpleValidationException()
        {
            var testItem = "          ";

            Assert.ThrowsAny<SimpleValidationException>(
                () => testItem.AsValidatable(nameof(testItem)).NotNullOrWhiteSpace());
        }

        [Fact]
        public void NullValue_Throws_SimpleValidationException()
        {
            var testItem = (string)null;

            Assert.ThrowsAny<SimpleValidationException>(
                () => testItem.AsValidatable(nameof(testItem)).NotNullOrWhiteSpace());
        }

        [Fact]
        public void Property_NotNullOrWhiteSpaceValue_SuccessfullyEnds()
        {
            var testItem = new TestItem
            {
                StringProperty = "valid string"
            };

            Assert.Equal(
                testItem.StringProperty,
                testItem.AsValidatable(nameof(testItem))
                    .ValidateProperty(item => (item.StringProperty, nameof(item.StringProperty)))
                    .NotNullOrWhiteSpace().Item);
        }

        [Fact]
        public void Property_EmptyValue_Throws_SimpleValidationException()
        {
            var testItem = new TestItem
            {
                StringProperty = string.Empty
            };

            Assert.ThrowsAny<SimpleValidationException>(
                () => testItem.AsValidatable(nameof(testItem))
                    .ValidateProperty(item => (item.StringProperty, nameof(item.StringProperty)))
                    .NotNullOrWhiteSpace());
        }

        [Fact]
        public void Property_WhiteSpacesValue_Throws_SimpleValidationException()
        {
            var testItem = new TestItem
            {
                StringProperty = "        "
            };

            Assert.ThrowsAny<SimpleValidationException>(
                () => testItem.AsValidatable(nameof(testItem))
                    .ValidateProperty(item => (item.StringProperty, nameof(item.StringProperty)))
                    .NotNullOrWhiteSpace());
        }

        [Fact]
        public void Property_NullValue_Throws_SimpleValidationException()
        {
            var testItem = new TestItem
            {
                StringProperty = null
            };

            Assert.ThrowsAny<SimpleValidationException>(
                () => testItem.AsValidatable(nameof(testItem))
                    .ValidateProperty(item => (item.StringProperty, nameof(item.StringProperty)))
                    .NotNullOrWhiteSpace());
        }
    }
}
