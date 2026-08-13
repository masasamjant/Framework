using Microsoft.AspNetCore.Http;

namespace Masasamjant.Web
{
    public abstract class UnitTest
    {
        protected static HttpContext CreateHttpContext(ISession? session = null)
        {
            var context = new DefaultHttpContext();
            if (session != null)
                context.Session = session;
            return context;
        }
    }
}
