namespace SimpleUtils.Validation.Common
{
    internal class ValidatableItem<T> : IValidatable<T>
    {
        public string ItemName { get; set; }

        public T Item { get; set; }

        public override string ToString()
        {
            return ItemName;
        }
    }
}
