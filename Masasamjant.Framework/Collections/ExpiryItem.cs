namespace Masasamjant.Collections
{
    internal sealed class ExpiryItem<T>
    {
        public ExpiryItem(T item, DateTime time) 
        { 
            Item = item;
            Time = time;
        }

        public T Item { get; }

        public DateTime Time { get; }

        public bool IsExpired(TimeSpan lifetime)
        {
            return DateTime.UtcNow - Time >= lifetime;
        }
    }
}
