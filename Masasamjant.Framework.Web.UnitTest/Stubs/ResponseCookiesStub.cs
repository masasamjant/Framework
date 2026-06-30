using Microsoft.AspNetCore.Http;

namespace Masasamjant.Web.Stubs
{
    internal class ResponseCookiesStub : IResponseCookies
    {
        private readonly List<CookieStub> cookies = new List<CookieStub>();

        public IEnumerable<CookieStub> Cookies
        {
            get
            {
                foreach (var cookie in cookies)
                    yield return cookie;
            }
        }

        public void Append(string key, string value)
        {
            Delete(key);
            var cookie = new CookieStub(key, value, null);
            cookies.Add(cookie);
        }

        public void Append(string key, string value, CookieOptions options)
        {
            Delete(key);
            var cookie = new CookieStub(key, value, options);
            cookies.Add(cookie);
        }

        public void Delete(string key)
        {
            var currentCookie = cookies.FirstOrDefault(x => x.Key == key);
            if (currentCookie != null)
                cookies.Remove(currentCookie);
        }

        public void Delete(string key, CookieOptions options)
        {
            Delete(key);
        }
    }
}
