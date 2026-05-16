using Masasamjant.Windows.Input;
using System.Diagnostics;

namespace Masasamjant.Windows.WinApi
{
    internal static class Hooks
    {
        internal const int WH_MOUSE_LL = 14;
        internal const int WH_MOUSE = 15;
        internal const int WH_KEYBOARD_LL = 13;
        internal const int WH_KEYBOARD = 2;

        internal static IntPtr GetHook(int hook, HookScope scope, HookProc proc)
        { 
            return scope == HookScope.Global
                ? GetGlobalHook(hook, proc)
                : GetLocalHook(hook, proc);
        }

        private static IntPtr GetGlobalHook(int hook, HookProc proc)
        {
            using (var process = Process.GetCurrentProcess())
            {
                var module = process.MainModule;

                if (module == null)
                    throw new InvalidOperationException("Could not get current process module.");

                using (module)
                    return ApiMethods.SetWindowsHookEx(hook, proc, ApiMethods.GetModuleHandle(module.ModuleName), 0);
            }
        }

        private static IntPtr GetLocalHook(int hook, HookProc proc)
        {
            return ApiMethods.SetWindowsHookEx(hook, proc, IntPtr.Zero, ApiMethods.GetCurrentThreadId());
        }
    }
}
