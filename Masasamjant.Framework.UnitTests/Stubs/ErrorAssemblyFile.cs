using Masasamjant.Reflection;
using System.Reflection;

namespace Masasamjant.Stubs
{
    internal class ErrorAssemblyFile : AssemblyFile
    {
        public ErrorAssemblyFile(string filePath)
            : base(filePath)
        { }

        protected override Assembly Load()
        {
            throw new NotImplementedException();
        }
    }
}
