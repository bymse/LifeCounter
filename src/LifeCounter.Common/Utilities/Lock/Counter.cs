namespace LifeCounter.Common.Utilities.Lock;

internal sealed class Counter<T>
{
    public Counter(T value)
    {
        Count = 1;
        Value = value;
    }

    public T Value { get; }
    public int Count { get; set; }
}