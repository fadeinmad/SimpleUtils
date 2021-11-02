using System;
using SimpleUtils.Validation.Common;
using SimpleUtils.Validation.Extensions;
using SimpleUtils.Validation.InnerValidation;
using SimpleUtils.Validation.UnitTests.Models;
using Xunit;

namespace SimpleUtils.Validation.UnitTests.ValidationExtensions
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

            var propertyValidator = testItem.AsValidatable(itemName)
                .ValidateProperty(item => (item.Property, propertyName));

            Assert.NotNull(propertyValidator);
            Assert.IsType<ValidatableProperty<TestItem, TestProperty>>(propertyValidator);
            Assert.IsAssignableFrom<IValidatableProperty<TestItem, TestProperty>>(propertyValidator);
            Assert.Equal(propertyValidator.ItemName, propertyName);
            Assert.Equal(propertyValidator.Item, testItem.Property);
            Assert.Equal(propertyValidator.ToString(), $"{itemName}.{propertyName}");

            var itemValidator = propertyValidator.EndProperty();

            Assert.NotNull(itemValidator);
            Assert.IsType<ValidatableItem<TestItem>>(itemValidator);
            Assert.IsAssignableFrom<IValidatable<TestItem>>(itemValidator);
            Assert.Equal(itemValidator.ItemName, itemName);
            Assert.Equal(itemValidator.Item, testItem);
        }

        [Fact]
        public void NullPropertySelector_Throws_SimpleValidationExtensionException()
        {
            var testItem = new TestItem
            {
                Property = new TestProperty()
            };
            const string itemName = nameof(testItem);

            Assert.Throws<SimpleValidationExtensionException>(
                () => testItem
                    .AsValidatable(itemName)
                    .ValidateProperty((Func<TestItem, (TestProperty, string)>)null));
        }

        [Fact]
        public void NullPropertyName_Throws_SimpleValidationExtensionException()
        {
            var testItem = new TestItem
            {
                Property = new TestProperty()
            };
            const string itemName = nameof(testItem);

            Assert.Throws<SimpleValidationExtensionException>(
                () => testItem
                    .AsValidatable(itemName)
                    .ValidateProperty(item => (item.Property, (string)null)));
        }

        [Fact]
        public void ValidSelectorAndAction_Returns_IValidatable()
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
                        (Func<TestItem, (TestProperty, string)>) null,
                        validator => { });
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
                        validator => { });
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
                        (Action<IValidatable<TestProperty>>)null);
            });
        }
    }
}