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
        public static explicit operator KeyboardArgs (IntPtr ptr)
            => Marshal.PtrToStructure<KeyboardArgs>(ptr);

        public uint TrueScanCode => Flags.HasFlag(KeyboardFlags.Extended) ? ScanCode + 0x100 : ScanCode;

        public uint Advanced => TrueScanCode << 8;
        public uint Hyrbid => Code + Advanced;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator RawKey (KeyboardArgs args)
        {
            // First check just the virtual key
            if (Enum.IsDefined(typeof(RawKey), args.Code))
                return (RawKey)args.Code;

            // If that fails, do advanced check against the scan code
            if (Enum.IsDefined(typeof(RawKey), args.Advanced))
                return (RawKey)(args.Advanced);

            // If it fails again, do one final check as a hyrbid of both the code and scancode
            if (Enum.IsDefined(typeof(RawKey), args.Hyrbid))
                return (RawKey)(args.Hyrbid);

            // If still nothing, just return the simple value
            return (RawKey)args.Code;
        }
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
        public UnityEngine.Vector2Int Point;
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
