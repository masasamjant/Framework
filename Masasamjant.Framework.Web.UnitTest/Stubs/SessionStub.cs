using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Web.Stubs
{
    internal class SessionStub : ISession
    {
        private readonly Guid id = Guid.NewGuid();
        private readonly Dictionary<string, byte[]> values = new Dictionary<string, byte[]>();

        public bool IsAvailable => true;

        public string Id => id.ToString();

        public IEnumerable<string> Keys => values.Keys;

        public void Clear()
        {
            values.Clear();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task LoadAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public void Remove(string key)
        {
            values.Remove(key);
        }

        public void Set(string key, byte[] value)
        {
            values[key] = value;
        }

        public bool TryGetValue(string key, [NotNullWhen(true)] out byte[]? value)
        {
            return values.TryGetValue(key, out value);
        }
    }
}
