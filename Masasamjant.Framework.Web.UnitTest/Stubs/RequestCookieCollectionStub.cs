using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masasamjant.Web.Stubs
{
    internal class RequestCookieCollectionStub : IRequestCookieCollection
    {
        private readonly Dictionary<string, string> cookies = new Dictionary<string, string>();

        public RequestCookieCollectionStub(Dictionary<string, string>? cookies = null)
        {
            if (cookies != null)
                this.cookies = new Dictionary<string, string>(cookies);
        }
    
        public string? this[string key] => cookies[key];

        public int Count => cookies.Count;

        public ICollection<string> Keys => cookies.Keys;

        public bool ContainsKey(string key)
        {
            return cookies.ContainsKey(key);    
        }

        public void Add(string key, string value)
            => cookies[key] = value;

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            foreach (var keyValue in cookies)
                yield return keyValue;
        }

        public bool TryGetValue(string key, [NotNullWhen(true)] out string? value)
        {
            return cookies.TryGetValue(key, out value); 
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
