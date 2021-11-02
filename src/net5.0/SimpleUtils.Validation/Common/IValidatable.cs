namespace SimpleUtils.Validation.Common
{
    public interface IValidatable<out T>
    {
        T Item { get; }

        string ItemName { get; }
    }
}
