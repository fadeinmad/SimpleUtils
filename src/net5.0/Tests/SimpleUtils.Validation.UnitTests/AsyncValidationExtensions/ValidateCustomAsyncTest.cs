using System;
using System.Threading.Tasks;

using SimpleUtils.Validation.Common;
using SimpleUtils.Validation.Common.Exceptions;
using SimpleUtils.Validation.Extensions;
using SimpleUtils.Validation.InnerValidation;
using SimpleUtils.Validation.UnitTests.Models;
using Xunit;

namespace SimpleUtils.Validation.UnitTests.AsyncValidationExtensions
{
    public sealed class ValidateCustomAsyncTest
    {
        [Fact]
        public async Task ValidCustomAction_Ends_WithSuccess()
        {
            var testItem = new TestItem();
            const string itemName = nameof(testItem);

            IValidatable<TestItem> validatorToCheck = null;
            await testItem.AsValidatable(itemName)
                .ValidateCustomAsync(
                    validator =>
                    {
                        validatorToCheck = validator;
                        return Task.CompletedTask;
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

            Assert.ThrowsAsync<SimpleValidationExtensionException>(
                async () =>
                {
                    await testItem.AsValidatable(itemName)
                        .ValidateCustomAsync(null);
                });
        }

        [Fact]
        public void CustomActionUnexpectedError_Throws_ValidateCustomUnexpectedException()
        {
            var testItem = new TestItem();
            const string itemName = nameof(testItem);

            Assert.ThrowsAsync<ValidateCustomUnexpectedException<TestItem>>(
                async () =>
                {
                    await testItem.AsValidatable(itemName)
                        .ValidateCustomAsync(validator => throw new Exception());
                });
        }

        [Fact]
        public void CustomActionError_Throws_SimpleValidationException()
        {
            var testItem = new TestItem();
            const string itemName = nameof(testItem);

            Assert.ThrowsAsync<SimpleValidationException<TestItem>>(
                async () =>
                {
                    await testItem.AsValidatable(itemName)
                        .ValidateCustomAsync(
                            validator =>
                            {
                                validator.FailValidation("Error");
                                return Task.CompletedTask;
                            });
                });
        }

        [Fact]
        public void Check_ValidateCustomUnexpectedException_IsInheritedFrom_SimpleValidationException()
        {
            var testItem = new TestItem();
            const string itemName = nameof(testItem);

            var validator = testItem.AsInnerValidatable(itemName);

            Assert.NotNull(validator);
            Assert.ThrowsAnyAsync<SimpleValidationException>(
                async () =>
                {
                   await  testItem.AsValidatable(itemName)
                        .ValidateCustomAsync(customValidator => throw new Exception());
                });
        }

    }
}
