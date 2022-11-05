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
        public static bool WorkInBackground { get; set; }
        /// <summary>
        /// Whether handled input messages should not be propagated further.
        /// </summary>
        public static bool InterceptMessages { get; set; }
        /// <summary>
        /// Currently pressed keys.
        /// </summary>
        public static IReadOnlyCollection<RawKey> PressedKeys => pressedKeys;

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
        /// Whether hooks are allowed to handle incoming inputs.
        /// </summary>
        public static bool CanHandleHook ()
        {
            if (WorkInBackground) return true;
#if UNITY_EDITOR
            // Restores the old functionality where focus on the editor period qualifies for
            // "on focus". Otherwise it would only react if the game view was focused. If this
            // behavior is desired, replace the below with the "isFocused" check
            return UnityEditorInternal.InternalEditorUtility.isApplicationActive;
#else
            return UnityEngine.Application.isFocused;
#endif
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
            return Win32API.SetWindowsHookEx(HookType.WH_KEYBOARD_LL, HandleLowLevelKeyboardProc, IntPtr.Zero, 0);
        }

        private static IntPtr SetMouseHook ()
        {
            return Win32API.SetWindowsHookEx(HookType.WH_MOUSE_LL, HandleLowLevelMouseProc, IntPtr.Zero, 0);
        }

        [MonoPInvokeCallback(typeof(Win32API.HookProc))]
        private static int HandleLowLevelKeyboardProc (int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0) return Win32API.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);

            var args = (KeyboardArgs)lParam;
            var state = (RawKeyState)wParam;
            var key = (RawKey)args;

            if (state == RawKeyState.KeyDown || state == RawKeyState.SysKeyDown) HandleKeyDown(key);
            else HandleKeyUp(key);

            return InterceptMessages ? 1 : Win32API.CallNextHookEx(IntPtr.Zero, 0, wParam, lParam);
        }

        [MonoPInvokeCallback(typeof(Win32API.HookProc))]
        private static int HandleLowLevelMouseProc (int code, IntPtr wParam, IntPtr lParam)
        {
            if (!CanHandleHook()) return 0;

            if (code < 0) return Win32API.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);

            var args = (MouseArgs)lParam;
            var state = (RawMouseState)wParam;
            switch (state)
            {
                case RawMouseState.LeftButtonDown: HandleKeyDown(RawKey.LeftButton); break;
                case RawMouseState.LeftButtonUp: HandleKeyUp(RawKey.LeftButton); break;
                case RawMouseState.RightButtonDown: HandleKeyDown(RawKey.RightButton); break;
                case RawMouseState.RightButtonUp: HandleKeyUp(RawKey.RightButton); break;
                case RawMouseState.MiddleButtonDown: HandleKeyDown(RawKey.MiddleButton); break;
                case RawMouseState.MiddleButtonUp: HandleKeyUp(RawKey.MiddleButton); break;
                case RawMouseState.ExtraButtonDown:
                    HandleKeyDown((short)(args.MouseData >> 16 & 0xFFFF) == 1
                    ? RawKey.ExtraButton1 : RawKey.ExtraButton2); break;
                case RawMouseState.ExtraButtonUp:
                    HandleKeyUp((short)(args.MouseData >> 16 & 0xFFFF) == 1
                    ? RawKey.ExtraButton1 : RawKey.ExtraButton2); break;
                case RawMouseState.MouseWheel:
                case RawMouseState.MouseWheelHorizontal:
                    short delta = (short)(args.MouseData >> 16 & 0xFFFF);
                    HandleKeyDown(state == RawMouseState.MouseWheel ?
                        delta < 0 ? RawKey.WheelDown : RawKey.WheelUp :
                        delta < 0 ? RawKey.WheelLeft : RawKey.WheelRight);
                    HandleKeyUp(state == RawMouseState.MouseWheel ?
                        delta < 0 ? RawKey.WheelDown : RawKey.WheelUp :
                        delta < 0 ? RawKey.WheelLeft : RawKey.WheelRight); break;
            }

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

        /// <summary>
        /// Ensures that any environment with UnityRawInput will have
        /// <a cref="UnityEngine.Application.runInBackground"/> enabled
        /// </summary>
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void EnsureBackground ()
        {
            if (UnityEngine.Application.runInBackground) return;
            UnityEngine.Debug.LogWarning("Application isn't set to run in background! Not enabling this option will " +
                "cause severe mouse slowdown if the window isn't in focus. Enabling behavior for this playsession, " +
                "but you should explicitly enable this in \"Build Settings→Player Settings→Player→Resolution and " +
                "Presentation→Run In Background\".");
            UnityEngine.Application.runInBackground = true;
        }
    }
}
