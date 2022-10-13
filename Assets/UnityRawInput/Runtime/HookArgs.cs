using System;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityRawInput
{
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardArgs
    {
        public uint Code;
        public uint ScanCode;
        public KeyboardFlags Flags;
        public uint Time;
        public UIntPtr ExtraInfo;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator KeyboardArgs (IntPtr ptr)
            => Marshal.PtrToStructure<KeyboardArgs>(ptr);
    }

    [Flags]
    public enum KeyboardFlags : uint
    {
        Extended = 0x01,
        LowerInjected = 0x02,
        Injected = 0x10,
        AltDown = 0x20,
        Up = 0x80
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MouseArgs
    {
        public Vector2Int Point;
        public uint MouseData;
        public MouseFlags Flags;
        public uint Time;
        public UIntPtr ExtraInfo;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator MouseArgs (IntPtr ptr)
            => Marshal.PtrToStructure<MouseArgs>(ptr);
    }

    [Flags]
    public enum MouseFlags : uint
    {
        Injected = 0x01,
        LowerInjected = 0x02
    }
}
