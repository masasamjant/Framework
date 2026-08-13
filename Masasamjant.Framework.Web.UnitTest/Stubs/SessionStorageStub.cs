namespace Masasamjant.Web.Stubs
{
    internal class SessionStorageStub : SessionStorage
    {
        private readonly Dictionary<string, string> items = new Dictionary<string, string>();
        private readonly Guid? identifier;
        private readonly string? sessionIdentifierKey;

        public SessionStorageStub(Guid? identifier = null, string? sessionIdentifierKey = null)
        {
            this.identifier = identifier;
            this.sessionIdentifierKey = sessionIdentifierKey;
        }

        public override void Clear()
        {
            items.Clear();
        }

        protected override string SessionIdentifierKey
        {
            get
            {
                return sessionIdentifierKey ?? base.SessionIdentifierKey;
            }
        }

        public string GetDefaultSessionIdentifierKey()
            => DefaultSessionIdentifierKey;

        public override string? GetString(string key)
        {
            return items.TryGetValue(key, out var value) ? value : null;
        }

        public override void Remove(string key)
        {
            items.Remove(key);
        }

        public override void SetString(string key, string value)
        {
            items[key] = value;
        }

        protected override string CreateSessionIdentifier()
        {
            if (identifier.HasValue)
                return identifier.Value.ToString();

            return base.CreateSessionIdentifier();
        }
    }
}
