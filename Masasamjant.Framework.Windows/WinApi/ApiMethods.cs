using System.Runtime.InteropServices;
using System.Text;

namespace Masasamjant.Windows.WinApi
{
    internal class ApiMethods
    {
        private const string User32 = "user32.dll";
        private const string Kernel32 = "kernel32.dll";

        [DllImport(User32, CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport(User32, CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport(User32, CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport(User32)]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport(User32)]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport(Kernel32)]
        internal static extern uint GetCurrentThreadId();

        [DllImport(User32)]
        internal static extern uint GetDoubleClickTime();
    }
}
