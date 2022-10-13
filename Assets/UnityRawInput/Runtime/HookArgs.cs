using System;
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
        public static explicit operator KeyboardArgs (IntPtr ptr) => (KeyboardArgs)Marshal.PtrToStructure(ptr, typeof(KeyboardArgs));
    }

    [Flags]
    public enum KeyboardFlags : uint
    {
        Extended = 0x01,
        Injected = 0x10,
        AltDown = 0x20,
        Up = 0x80,
    }
}
