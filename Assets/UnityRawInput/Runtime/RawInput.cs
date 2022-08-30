using System;
using System.Collections.Generic;
using AOT;

namespace UnityRawInput
{
    public static class RawInput
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
        public static bool IsRunning => hooks.Count > 0;
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

        /// <summary>
        /// Initializes the service and starts processing input messages.
        /// </summary>
        /// <param name="workInBackground">Whether input messages should be handled when the application is not in focus.</param>
        /// <returns>Whether the service started successfully.</returns>
        public static bool Start (bool workInBackground)
        {
            if (IsRunning) return false;
            WorkInBackground = workInBackground;
            SetHooks();
            return hooks.TrueForAll(h => h != IntPtr.Zero);
        }

        /// <summary>
        /// Terminates the service and stops processing input messages.
        /// </summary>
        public static void Stop ()
        {
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

        private static void SetHooks ()
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

            var args = KeyboardArgs.FromPtr(lParam);
            var state = (RawKeyState)wParam;
            var key = (RawKey)args.Code;

            if (state == RawKeyState.KeyDown || state == RawKeyState.SysKeyDown) HandleKeyDown(key);
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
            else if (state == RawMouseState.ExtraButtonDown) HandleKeyDown(RawKey.ExtraButton1);
            else if (state == RawMouseState.LeftButtonUp) HandleKeyUp(RawKey.LeftButton);
            else if (state == RawMouseState.MiddleButtonUp) HandleKeyUp(RawKey.MiddleButton);
            else if (state == RawMouseState.RightButtonUp) HandleKeyUp(RawKey.RightButton);
            else if (state == RawMouseState.ExtraButtonUp) HandleKeyUp(RawKey.ExtraButton1);
            else return Win32API.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            return InterceptMessages ? 1 : Win32API.CallNextHookEx(IntPtr.Zero, 0, wParam, lParam);
        }

        private static void HandleKeyDown (RawKey key)
        {
            var added = pressedKeys.Add(key);
            if (added) OnKeyDown?.Invoke(key);
        }

        private static void HandleKeyUp (RawKey key)
        {
            pressedKeys.Remove(key);
            OnKeyUp?.Invoke(key);
        }
    }
}
