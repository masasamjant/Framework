using Microsoft.AspNetCore.Http;

namespace Masasamjant.Web.Stubs
{
    internal class CookieStub
    {
        public CookieStub(string key, string value, CookieOptions? options)
        {
            Key = key;
            Value = value;
            Options = options;
        }

        public string Key { get; }

        public string Value { get; }

        public CookieOptions? Options { get; }
    }
}
