using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
namespace UnityRawInput
{
    public static class RawMouseButtons
    {
        private static LowLevelMouseProc _proc = HookCallback;
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static IntPtr hookPtr
            = IntPtr.Zero;
        private static bool IsRunning;
        private static bool WorkInBackground;

        public static event Action<RawClicks> OnMouseDown;
        /// <summary>
        /// Event invoked when user releases a key.
        /// </summary>
        public static event Action<RawClicks> OnMouseUp;


        private static HashSet<RawClicks> pressedKeys = new HashSet<RawClicks>();
        public static bool Start(bool workInBackround)
        {
            if (hookPtr == IntPtr.Zero)
            {
                if (workInBackround) hookPtr = SetWindowsHookEx(((int)HookType.WH_MOUSE_LL), _proc, IntPtr.Zero, 0);
                else hookPtr = SetWindowsHookEx(((int)HookType.WH_MOUSE), _proc, IntPtr.Zero, (uint)Win32API.GetCurrentThreadId());
            }
            if (hookPtr == IntPtr.Zero) return false;

            return true;
        }
        public static void Stop()
        {
                if (hookPtr != IntPtr.Zero)
                {
                    Win32API.UnhookWindowsHookEx(hookPtr);
                    hookPtr = IntPtr.Zero;
                }
            
        }

        /// <summary>
        /// Terminates the service and stops processing input messages.
        /// </summary>

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            RawClicks click = (RawClicks)wParam;
            if(click == RawClicks.WM_MOUSEMOVE)
                return CallNextHookEx(hookPtr, nCode, wParam, lParam);

            if (click == RawClicks.WM_RBUTTONUP || click == RawClicks.WM_MBUTTONUP|| click == RawClicks.WM_LBUTTONUP) HandleMouseUp(click);
            else
                HandleMouseDown(click);
                return CallNextHookEx(hookPtr, nCode, wParam, lParam);
        }










        [DllImport("User32")]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);


        [DllImport("User32")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        private static void HandleMouseDown(RawClicks click)
        {
            var added = pressedKeys.Add(click);
            if (added && OnMouseDown != null) OnMouseDown.Invoke(click);
        }

        private static void HandleMouseUp(RawClicks click)
        {
            click--;

            if (!pressedKeys.Contains(click))
                HandleMouseDown(click);
            pressedKeys.Remove(click);
            if (OnMouseUp != null) OnMouseUp.Invoke(click);
        }
    }

   
}
