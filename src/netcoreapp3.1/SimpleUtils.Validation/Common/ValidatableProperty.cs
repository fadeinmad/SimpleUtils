namespace SimpleUtils.Validation.Common
{
    internal class ValidatableProperty<TParent, TProperty> : IValidatableProperty<TParent, TProperty>
    {
        private readonly IValidatable<TParent> _parent;

        public ValidatableProperty(IValidatable<TParent> parent)
        {
            this._parent = parent;
        }

        public string ItemName { get; set; }

        public TProperty Item { get; set; }

        public IValidatable<TParent> EndProperty() => _parent;

        public override string ToString()
        {
            return $"{_parent.ItemName}.{ItemName}";
        }
    }
}
