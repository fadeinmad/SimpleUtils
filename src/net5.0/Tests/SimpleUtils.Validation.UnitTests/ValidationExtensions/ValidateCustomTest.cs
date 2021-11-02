using System;
using SimpleUtils.Validation.Common;
using SimpleUtils.Validation.Common.Exceptions;
using SimpleUtils.Validation.Extensions;
using SimpleUtils.Validation.InnerValidation;
using SimpleUtils.Validation.UnitTests.Models;
using Xunit;

namespace SimpleUtils.Validation.UnitTests.ValidationExtensions
{
    public sealed class ValidateCustomTest
    {
        [Fact]
        public void ValidCustomAction_Ends_WithSuccess()
        {
            var testItem = new TestItem();
            const string itemName = nameof(testItem);

            IValidatable<TestItem> validatorToCheck = null;
            testItem.AsValidatable(itemName)
                .ValidateCustom(validator =>
                {
                    validatorToCheck = validator;
                });

            Assert.NotNull(validatorToCheck);
            Assert.IsAssignableFrom<IValidatable<TestItem>>(validatorToCheck);
            Assert.Equal(validatorToCheck.ItemName, itemName);
            Assert.Equal(validatorToCheck.Item, testItem);
            Assert.Equal(validatorToCheck.ToString(), itemName);
        }

        [Fact]
        public void NullCustomAction_Throws_SimpleValidationExtensionException()
        {
            var testItem = new TestItem();
            const string itemName = nameof(testItem);

            Assert.Throws<SimpleValidationExtensionException>(
                () =>
                {
                    testItem.AsValidatable(itemName)
                        .ValidateCustom(null);
                });
        }

        [Fact]
        public void CustomActionUnexpectedError_Throws_ValidateCustomUnexpectedException()
        {
            var testItem = new TestItem();
            const string itemName = nameof(testItem);

            Assert.Throws<ValidateCustomUnexpectedException<TestItem>>(
                () =>
                {
                    testItem.AsValidatable(itemName)
                        .ValidateCustom(validator => throw new Exception());
                });
        }

        [Fact]
        public void CustomActionError_Throws_SimpleValidationException()
        {
            var testItem = new TestItem();
            const string itemName = nameof(testItem);

            Assert.Throws<SimpleValidationException<TestItem>>(
                () =>
                {
                    testItem.AsValidatable(itemName)
                        .ValidateCustom(validator => validator.FailValidation("Error"));
                });
        }

        [Fact]
        public void Check_ValidateCustomUnexpectedException_IsInheritedFrom_SimpleValidationException()
        {
            var testItem = new TestItem();
            const string itemName = nameof(testItem);

            var validator = testItem.AsInnerValidatable(itemName);

            Assert.NotNull(validator);
            Assert.ThrowsAny<SimpleValidationException>(
                () =>
                {
                    testItem.AsValidatable(itemName)
                        .ValidateCustom(customValidator => throw new Exception());
                });
        }
    }
}
