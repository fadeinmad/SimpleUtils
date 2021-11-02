using SimpleUtils.Validation.Common.Exceptions;
using SimpleUtils.Validation.Extensions;
using SimpleUtils.Validation.InnerValidation;
using SimpleUtils.Validation.UnitTests.Models;
using Xunit;

namespace SimpleUtils.Validation.UnitTests.ValidationExtensions
{
    public sealed class FailValidationTest
    {
        [Fact]
        public void InnerValidation_Throws_SimpleValidationExtensionException()
        {
            var testItem = new TestItem();
            const string itemName = nameof(testItem);

            var validator = testItem.AsInnerValidatable(itemName);

            Assert.NotNull(validator);
            Assert.Throws<SimpleValidationExtensionException>(
                () => validator.FailValidation("Error"));
        }

        [Fact]
        public void Validation_Throws_SimpleValidationException()
        {
            var testItem = new TestItem();
            const string itemName = nameof(testItem);

            var validator = testItem.AsValidatable(itemName);

            Assert.NotNull(validator);
            Assert.Throws<SimpleValidationException<TestItem>>(
                () => validator.FailValidation("Error"));
        }

        [Fact]
        public void AnyValidation_WithNullMessage_Throws_SimpleValidationExtensionException()
        {
            var testItem = new TestItem();
            const string itemName = nameof(testItem);

            var validator = testItem.AsValidatable(itemName);

            Assert.NotNull(validator);
            Assert.Throws<SimpleValidationExtensionException>(
                () => validator.FailValidation(null));
        }
    }
}
