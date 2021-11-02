using SimpleUtils.Validation.Common.Exceptions;
using SimpleUtils.Validation.Extensions;
using SimpleUtils.Validation.UnitTests.Models;
using Xunit;

namespace SimpleUtils.Validation.UnitTests.ObjectValidationExtensions
{
    public sealed class NotNullTest
    {
        [Fact]
        public void ValidItem_SuccessfullyEnds()
        {
            var itemToValidate = new TestItem();

            Assert.NotNull(itemToValidate.AsValidatable(nameof(itemToValidate)).NotNull());
        }

        [Fact]
        public void NullItem_Throws_SimpleValidationException()
        {
            TestItem testItem = null;

            Assert.ThrowsAny<SimpleValidationException>(
                () => testItem.AsValidatable(nameof(testItem))
                    .NotNull());
        }


        [Fact]
        public void Property_ValidItem_SuccessfullyEnds()
        {
            var itemToValidate = new TestItem
            {
                Property = new TestProperty()
            };

            Assert.NotNull(itemToValidate.AsValidatable(nameof(itemToValidate))
                .ValidateProperty(item => (item.Property, nameof(item.Property)))
                .NotNull());
        }

        [Fact]
        public void Property_NullItem_Throws_SimpleValidationException()
        {
            var itemToValidate = new TestItem();
            
            Assert.ThrowsAny<SimpleValidationException>(
                () => itemToValidate.AsValidatable(nameof(itemToValidate))
                .ValidateProperty(item => (item.Property, nameof(item.Property)))
                .NotNull());
        }
    }
}
