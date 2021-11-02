using System;
using System.Threading.Tasks;
using SimpleUtils.Validation.Common;
using SimpleUtils.Validation.Extensions;
using SimpleUtils.Validation.InnerValidation;
using SimpleUtils.Validation.UnitTests.Models;
using Xunit;

namespace SimpleUtils.Validation.UnitTests.AsyncValidationExtensions
{
    public sealed class ValidatePropertyTest
    {
        [Fact]
        public void ValidSelector_Returns_IValidatableProperty()
        {
            var testItem = new TestItem
            {
                Property = new TestProperty()
            };
            const string itemName = nameof(testItem);
            const string propertyName = nameof(testItem.Property);

            IValidatable<TestProperty> propertyValidatorToCheck = null;
            var itemValidator = testItem.AsValidatable(itemName)
                .ValidateProperty(
                    item => (item.Property, propertyName),
                    validator =>
                    {
                        propertyValidatorToCheck = validator;
                        return Task.CompletedTask;
                    });

            Assert.NotNull(itemValidator);
            Assert.IsType<ValidatableItem<TestItem>>(itemValidator);
            Assert.IsAssignableFrom<IValidatable<TestItem>>(itemValidator);
            Assert.Equal(itemValidator.ItemName, itemName);
            Assert.Equal(itemValidator.Item, testItem);

            Assert.NotNull(propertyValidatorToCheck);
            Assert.IsType<ValidatableItem<TestProperty>>(propertyValidatorToCheck);
            Assert.IsAssignableFrom<IValidatable<TestProperty>>(propertyValidatorToCheck);
            Assert.Equal(propertyValidatorToCheck.ItemName, $"{itemName}.{propertyName}");
            Assert.Equal(propertyValidatorToCheck.ToString(), $"{itemName}.{propertyName}");
            Assert.Equal(propertyValidatorToCheck.Item, testItem.Property);
        }

        [Fact]
        public void NullSelectorAndAnyAction_Throws_SimpleValidationExtensionException()
        {
            var testItem = new TestItem
            {
                Property = new TestProperty()
            };
            const string itemName = nameof(testItem);

            Assert.Throws<SimpleValidationExtensionException>(() =>
            {
                testItem.AsValidatable(itemName)
                    .ValidateProperty(
                        (Func<TestItem, (TestProperty, string)>)null,
                        validator => Task.CompletedTask);
            });
        }

        [Fact]
        public void NullPropertyNameInSelectorAndAnyAction_Throws_SimpleValidationExtensionException()
        {
            var testItem = new TestItem
            {
                Property = new TestProperty()
            };
            const string itemName = nameof(testItem);

            Assert.Throws<SimpleValidationExtensionException>(() =>
            {
                testItem.AsValidatable(itemName)
                    .ValidateProperty(
                        item => (item.Property, (string)null),
                        validator => Task.CompletedTask);
            });
        }

        [Fact]
        public void ValidSelectorAndNullAction_Throws_SimpleValidationExtensionException()
        {
            var testItem = new TestItem
            {
                Property = new TestProperty()
            };

            Assert.Throws<SimpleValidationExtensionException>(() =>
            {
                testItem.AsValidatable(nameof(testItem))
                    .ValidateProperty(
                        item => (item.Property, nameof(item.Property)),
                        (Func<IValidatable<TestProperty>, Task>)null);
            });
        }
    }
}
