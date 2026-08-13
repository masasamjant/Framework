using Masasamjant.Windows.WinApi;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace Masasamjant.Windows.Input
{
    /// <summary>
    /// Represents hook to capture mouse related messages and raise mouse events.
    /// The instance should be disposed immediately when no longer needed to release the hook.
    /// </summary>
    public sealed class MouseHook : InputDevice, IMouse, IHook
    {
        private IntPtr hook = IntPtr.Zero;
        private readonly ConcurrentDictionary<MouseButton, MouseClickCounter> pressedButtons = new ConcurrentDictionary<MouseButton, MouseClickCounter>();
        private MouseLocation? previousLocation;

        /// <summary>
        /// Occurs when mouse move.
        /// </summary>
        public event EventHandler<MouseEventArgs>? Move;

        /// <summary>
        /// Occurs when mouse button is pressed down.
        /// </summary>
        public event EventHandler<MouseEventArgs>? ButtonPressed;

        /// <summary>
        /// Occurs when mouse button is released up.
        /// </summary>
        public event EventHandler<MouseEventArgs>? ButtonReleased;

        /// <summary>
        /// Occurs when mouse button is released up.
        /// </summary>
        public event EventHandler<MouseEventArgs>? ButtonClicked;

        /// <summary>
        /// Occurs when mouse button is double clicked.
        /// </summary>
        public event EventHandler<MouseEventArgs>? ButtonDoubleClicked;

        /// <summary>
        /// Creates and sets up a mouse hook with the specified scope. Remember to dispose the returned instance when done to release the hook.
        /// </summary>
        /// <param name="scope">The scope of the hook, indicating whether it is local to the current thread or global across the entire system.</param>
        /// <returns>A <see cref="MouseHook"/> instance.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="scope"/> is not defined.</exception>
        public static MouseHook Create(HookScope scope)
        {
            if (!Enum.IsDefined(scope))
                throw new ArgumentException("Invalid hook scope.", nameof(scope));

            var mouse = new MouseHook(scope);
            mouse.SetHook();
            return mouse;
        }

        /// <summary>
        /// Gets the mouse options.
        /// </summary>
        public MouseOptions Options { get; } = new MouseOptions();

        /// <summary>
        /// Gets the scope of the hook, 
        /// indicating whether it is local to the current thread or global across the entire system.
        /// </summary>
        public HookScope Scope { get; }

        /// <summary>
        /// Disposes current instance.
        /// </summary>
        /// <param name="disposing"><c>true</c> if disposing; <c>false</c> otherwise.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (hook != IntPtr.Zero)
                {
                    ApiMethods.UnhookWindowsHookEx(hook);
                    hook = IntPtr.Zero;
                }
                base.Dispose(disposing);
            }
        }

        private MouseHook(HookScope scope)
        {
            Scope = scope;
        }

        ~MouseHook()
        {
            Dispose(false);
        }

        private void SetHook()
        {
            if (Scope == HookScope.Global)
                hook = Hooks.GetHook(Hooks.WH_MOUSE_LL, Scope, HookProcCallback);
            else
                hook = Hooks.GetHook(Hooks.WH_MOUSE, Scope, HookProcCallback);
        }

        private IntPtr HookProcCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var hookData = GetMouseStruct(lParam);
                var message = wParam.ToInt32();

                switch (message)
                {
                    case Messages.WM_LBUTTONDOWN:
                        ProcessButtonDown(MouseButton.Left, hookData);
                        break;
                    case Messages.WM_LBUTTONUP:
                        ProcessButtonUp(MouseButton.Left, hookData);
                        break;
                    case Messages.WM_RBUTTONDOWN:
                        ProcessButtonDown(MouseButton.Right, hookData);
                        break;
                    case Messages.WM_RBUTTONUP:
                        ProcessButtonUp(MouseButton.Right, hookData);
                        break;
                    case Messages.WM_MBUTTONDOWN:
                        ProcessButtonDown(MouseButton.Middle, hookData);
                        break;
                    case Messages.WM_MBUTTONUP:
                        ProcessButtonUp(MouseButton.Middle, hookData);
                        break;
                    case Messages.WM_XBUTTONDOWN:
                        var xButton = (hookData.mouseData >> 16) & 0xFFFF;
                        ProcessButtonDown(xButton == 1 ? MouseButton.XButton1 : MouseButton.XButton2, hookData);
                        break;
                    case Messages.WM_XBUTTONUP:
                        var xButtonUp = (hookData.mouseData >> 16) & 0xFFFF;
                        ProcessButtonUp(xButtonUp == 1 ? MouseButton.XButton1 : MouseButton.XButton2, hookData);
                        break;
                    case Messages.WM_MOUSEMOVE:
                        ProcessMouseMove(hookData);
                        break;
                }
            }

            return ApiMethods.CallNextHookEx(hook, nCode, wParam, lParam);
        }

        private static MSLLHOOKSTRUCT GetMouseStruct(IntPtr lParam)
        {
            return Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);
        }

        private void ProcessButtonDown(MouseButton button, MSLLHOOKSTRUCT hookData)
        {
            var location = GetMouseLocation(hookData);
            var args = new MouseEventArgs(location, new MouseButtonInfo(button, MouseButtonState.Down));
            if (IsClickTracked())
                pressedButtons.TryAdd(button, new MouseClickCounter(OnClick, button));
            ButtonPressed?.Invoke(this, args);
        }

        private void OnClick(MouseButton button, MouseLocation location, int clickCount)
        {
            if (pressedButtons.TryRemove(button, out var context))
            {
                context.Dispose();

                if (clickCount > 1 && !Options.IsDoubleClickSuspended)
                    ButtonDoubleClicked?.Invoke(this, new MouseEventArgs(location, new MouseButtonInfo(button, MouseButtonState.Up)));
                else if (clickCount == 1 && !Options.IsClickSuspended)
                    ButtonClicked?.Invoke(this, new MouseEventArgs(location, new MouseButtonInfo(button, MouseButtonState.Up)));
            }
        }

        private void ProcessButtonUp(MouseButton button, MSLLHOOKSTRUCT hookData)
        {
            var location = GetMouseLocation(hookData);
            var args = new MouseEventArgs(location, new MouseButtonInfo(button, MouseButtonState.Up));
            ButtonReleased?.Invoke(this, args);

            if (args.IsHandled)
            {
                if (pressedButtons.TryRemove(button, out var counter))
                    counter.Dispose();
            }
            else if (pressedButtons.TryGetValue(button, out var counter))
            {
                counter.Click(location);
            }
        }

        private void ProcessMouseMove(MSLLHOOKSTRUCT hookData)
        {
            var location = GetMouseLocation(hookData);

            if (Options.IsMoveSuspended ||!IsMove(location))
                return;

            var mouseButton = pressedButtons.FirstOrDefault();
            MouseButtonInfo? button = null;
            if (mouseButton.Key != MouseButton.None)
                button = new MouseButtonInfo(mouseButton.Key, MouseButtonState.Down);
            var args = new MouseEventArgs(location, button);
            Move?.Invoke(this, args);
            if (args.IsHandled)
                return;
        }

        private bool IsMove(MouseLocation currentLocation)
        {
            if (previousLocation.HasValue)
            {
                var prev = previousLocation.Value;
                int threshold = Options.MoveThreshold;

                if (threshold > 0)
                {
                   if (Math.Abs(prev.X - currentLocation.X) > threshold || 
                       Math.Abs(prev.Y - currentLocation.Y) > threshold)
                    {
                        previousLocation = currentLocation;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                    previousLocation = currentLocation;
            }
            else
                previousLocation = currentLocation;

            return true;
        }

        private static MouseLocation GetMouseLocation(MSLLHOOKSTRUCT hookData)
            => new MouseLocation(hookData.pt.x, hookData.pt.y);

        private bool IsClickTracked()
        {
            return Options.IsClickSuspended == false || Options.IsDoubleClickSuspended == false;
        }

        private class MouseClickCounter : IDisposable
        {
            private int clickCount;
            private Action<MouseButton, MouseLocation, int>? elapsedCallback;
            private System.Timers.Timer? timer;
            private readonly MouseButton button;
            private MouseLocation location = new MouseLocation(0, 0);

            public MouseClickCounter(Action<MouseButton, MouseLocation, int> elapsedCallback, MouseButton button)
            {
                this.button = button;
                this.elapsedCallback = elapsedCallback;
                clickCount = 0;
                timer = new System.Timers.Timer(ApiMethods.GetDoubleClickTime());
                timer.AutoReset = false;
                timer.Elapsed += OnTimerElapsed;
                timer.Start();
            }

            public void Click(MouseLocation location)
            {
                clickCount++;
                this.location = location;
            }

            private void OnTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
            {
                elapsedCallback?.Invoke(button, location, clickCount);
            }

            public void Dispose()
            {
                if (timer?.Enabled == true)
                    timer.Stop();

                if (timer != null)
                {
                    timer.Elapsed -= OnTimerElapsed;
                    timer.Dispose();
                    timer = null;
                }

                elapsedCallback = null;
            }
        }
    }
}
