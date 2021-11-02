namespace SimpleUtils.Validation.Common
{
    public interface IValidatableProperty<TParent, TProperty> : IValidatable<TProperty>
    {
        IValidatable<TParent> EndProperty();
    }
}
