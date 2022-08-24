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

        public static event Action<RawKey> OnMouseDown;
        /// <summary>
        /// Event invoked when user releases a key.
        /// </summary>
        public static event Action<RawKey> OnMouseUp;


        private static HashSet<RawKey> pressedKeys = new HashSet<RawKey>();
        public static bool Start(bool workInBackround)
        {
            if (hookPtr == IntPtr.Zero)
            {
                if (workInBackround) hookPtr = SetWindowsHookEx(((int)HookType.WH_MOUSE_LL), _proc, IntPtr.Zero, 0);
                else hookPtr = SetWindowsHookEx(((int)HookType.WH_MOUSE_LL), _proc, IntPtr.Zero, (uint)Win32API.GetCurrentThreadId());
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
            if (nCode > -0.00001f && (MouseMessages.WM_LBUTTONDOWN == (MouseMessages)wParam))
            {
                HandleMouseDown(RawKey.LeftButton);
            }
            if (nCode > -0.00001f && (MouseMessages.WM_RBUTTONDOWN == (MouseMessages)wParam))
            {
                HandleMouseDown(RawKey.RightButton);
            }
            if (nCode > -0.00001f && (MouseMessages.WM_LBUTTONUP == (MouseMessages)wParam))
            {
                HandleMouseUp(RawKey.LeftButton);
            }
            if (nCode > -0.00001f && (MouseMessages.WM_LBUTTONDOWN == (MouseMessages)wParam))
            {
                HandleMouseUp(RawKey.RightButton);
            }
            if (nCode > -0.00001f && (MouseMessages.WM_LBUTTONDOWN == (MouseMessages)wParam))
            {
                HandleMouseUp(RawKey.RightButton);
            }
            if (nCode > -0.00001f && (MouseMessages.WM_MBUTTONDOWN == (MouseMessages)wParam))
            {
                HandleMouseUp(RawKey.MiddleButton);
            }
            if (nCode > -0.00001f && (MouseMessages.WM_MBUTTONUP == (MouseMessages)wParam))
            {
                HandleMouseUp(RawKey.MiddleButton);
            }

            return CallNextHookEx(hookPtr, nCode, wParam, lParam);
        }




        private enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEWHEEL = 0x020A,
            WM_MBUTTONDOWN = 0x0207,
            WM_MBUTTONUP = 0x0208,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        }


        private struct POINT
        {
            public int x;
            public int y;
        }


        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [DllImport("User32")]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);


        [DllImport("User32")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        public static void HandleMouseDown(RawKey key)
        {
            var added = pressedKeys.Add(key);
            if (added && OnMouseDown != null) OnMouseDown.Invoke(key);
        }

        public static void HandleMouseUp(RawKey key)
        {
            pressedKeys.Remove(key);
            if (OnMouseUp != null) OnMouseUp.Invoke(key);
        }
    }

   
}
