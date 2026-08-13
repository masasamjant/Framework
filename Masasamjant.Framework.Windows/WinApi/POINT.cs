using System.Runtime.InteropServices;

namespace Masasamjant.Windows.WinApi
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct POINT
    {
        public int x;
        public int y;
    }
}
