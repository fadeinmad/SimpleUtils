using SimpleUtils.Validation.Common;

namespace SimpleUtils.Validation.InnerValidation
{
    internal sealed class InnerValidatableItem<TItem> : IValidatable<TItem>
    {
        public TItem Item { get; set; }

        public string ItemName { get; set; }
    }
}
