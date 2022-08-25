using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AOT;

namespace UnityRawInput
{
    public static class RawKeyInput
    {
        /// <summary>
        /// Event invoked when user presses a key.
        /// </summary>
        public static event Action<RawKey> OnKeyDown;
        /// <summary>
        /// Event invoked when user releases a key.
        /// </summary>
        public static event Action<RawKey> OnKeyUp;

        /// <summary>
        /// Whether the service is running and input messages are being processed.
        /// </summary>
        public static bool IsRunning => cts != null && !cts.IsCancellationRequested;
        /// <summary>
        /// Whether any key is currently pressed.
        /// </summary>
        public static bool AnyKeyDown => pressedKeys.Count > 0;
        /// <summary>
        /// Whether input messages should be handled when the application is not in focus.
        /// </summary>
        public static bool WorkInBackground { get; private set; }
        /// <summary>
        /// Whether handled input messages should not be propagated further.
        /// </summary>
        public static bool InterceptMessages { get; set; }

        private static readonly HashSet<RawKey> pressedKeys = new HashSet<RawKey>();
        private static readonly List<IntPtr> hooks = new List<IntPtr>();
        private static SynchronizationContext unityContext;
        private static CancellationTokenSource cts;

        /// <summary>
        /// Initializes the service and starts processing input messages.
        /// </summary>
        /// <param name="workInBackground">Whether input messages should be handled when the application is not in focus.</param>
        /// <param name="async">Whether to start the service in a background thread.</param>
        public static void Start (bool workInBackground, bool async)
        {
            if (IsRunning) return;
            cts = new CancellationTokenSource();
            unityContext = SynchronizationContext.Current;
            WorkInBackground = workInBackground;
            if (async)
                try { Task.Run(() => ListenHooksAsync(cts.Token)); }
                catch (OperationCanceledException) { }
            else ListenHooks();
        }

        /// <summary>
        /// Terminates the service and stops processing input messages.
        /// </summary>
        public static void Stop ()
        {
            cts?.Cancel();
            RemoveHooks();
            pressedKeys.Clear();
        }

        /// <summary>
        /// Checks whether provided key is currently pressed.
        /// </summary>
        public static bool IsKeyDown (RawKey key)
        {
            return pressedKeys.Contains(key);
        }

        private static async Task ListenHooksAsync (CancellationToken token)
        {
            ListenHooks();
            while (!token.IsCancellationRequested)
                await Task.Delay(10, token);
        }

        private static void ListenHooks ()
        {
            hooks.Add(SetKeyboardHook());
            hooks.Add(SetMouseHook());
        }

        private static void RemoveHooks ()
        {
            foreach (var pointer in hooks)
                if (pointer != IntPtr.Zero)
                    Win32API.UnhookWindowsHookEx(pointer);
            hooks.Clear();
        }

        private static IntPtr SetKeyboardHook ()
        {
            if (WorkInBackground) return Win32API.SetWindowsHookEx(HookType.WH_KEYBOARD_LL, HandleLowLevelKeyboardProc, IntPtr.Zero, 0);
            return Win32API.SetWindowsHookEx(HookType.WH_KEYBOARD, HandleKeyboardProc, IntPtr.Zero, (int)Win32API.GetCurrentThreadId());
        }

        private static IntPtr SetMouseHook ()
        {
            if (WorkInBackground) return Win32API.SetWindowsHookEx(HookType.WH_MOUSE_LL, HandleMouseProc, IntPtr.Zero, 0);
            return Win32API.SetWindowsHookEx(HookType.WH_MOUSE, HandleMouseProc, IntPtr.Zero, (int)Win32API.GetCurrentThreadId());
        }

        [MonoPInvokeCallback(typeof(Win32API.HookProc))]
        private static int HandleKeyboardProc (int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0) return Win32API.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);

            var isKeyDown = ((int)lParam & (1 << 31)) == 0;
            var key = (RawKey)wParam;

            if (isKeyDown) HandleKeyDown(key);
            else HandleKeyUp(key);

            return InterceptMessages ? 1 : Win32API.CallNextHookEx(IntPtr.Zero, 0, wParam, lParam);
        }

        [MonoPInvokeCallback(typeof(Win32API.HookProc))]
        private static int HandleLowLevelKeyboardProc (int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0) return Win32API.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);

            var kbd = KeyboardArgs.FromPtr(lParam);
            var keyState = (RawKeyState)wParam;
            var key = (RawKey)kbd.Code;

            if (keyState == RawKeyState.KeyDown || keyState == RawKeyState.SysKeyDown) HandleKeyDown(key);
            else HandleKeyUp(key);

            return InterceptMessages ? 1 : Win32API.CallNextHookEx(IntPtr.Zero, 0, wParam, lParam);
        }

        [MonoPInvokeCallback(typeof(Win32API.HookProc))]
        private static int HandleMouseProc (int code, IntPtr wParam, IntPtr lParam)
        {
           if (code < 0) return Win32API.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            var state = (RawMouseState)wParam;
            if (state == RawMouseState.LeftButtonDown) HandleKeyDown(RawKey.LeftButton);
            else if (state == RawMouseState.MiddleButtonDown) HandleKeyDown(RawKey.MiddleButton);
            else if (state == RawMouseState.RightButtonDown) HandleKeyDown(RawKey.RightButton);
            else if (state == RawMouseState.LeftButtonUp) HandleKeyUp(RawKey.LeftButton);
            else if (state == RawMouseState.MiddleButtonUp) HandleKeyUp(RawKey.MiddleButton);
            else if (state == RawMouseState.RightButtonUp) HandleKeyUp(RawKey.RightButton);
            else return Win32API.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            return Win32API.CallNextHookEx(IntPtr.Zero, 0, wParam, lParam);
        }

        private static void HandleKeyDown (RawKey key)
        {
            var added = pressedKeys.Add(key);
            if (added) unityContext.Send(InvokeOnUnityThread, key);
            void InvokeOnUnityThread (object obj) => OnKeyDown?.Invoke((RawKey)obj);
        }

        private static void HandleKeyUp (RawKey key)
        {
            if (!pressedKeys.Contains(key))
                HandleKeyDown(key);

            pressedKeys.Remove(key);
            unityContext.Send(InvokeOnUnityThread, key);
            void InvokeOnUnityThread (object obj) => OnKeyUp?.Invoke((RawKey)obj);
        }
    }
}
