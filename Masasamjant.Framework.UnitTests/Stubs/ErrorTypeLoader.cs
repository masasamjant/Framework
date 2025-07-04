using Masasamjant.Reflection;
using System.Reflection;

namespace Masasamjant.Stubs
{
    internal class ErrorTypeLoader : TypeLoader
    {
        protected override Type? GetType(Assembly assembly, string typeName)
        {
            throw new NotImplementedException();
        }
    }
}
