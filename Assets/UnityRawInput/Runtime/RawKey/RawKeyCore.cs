using System;
using System.Runtime.CompilerServices;

namespace UnityRawInput
{
    public partial struct RawKey : IEquatable<RawKey>, IFormattable
    {
        private readonly uint rawValue;
        /// <summary>
        /// Raw byte representation of a RawKey
        /// </summary>
        public uint RawValue => rawValue;

        /// <summary>
        /// Associated Virtual Key of a RawKey
        /// </summary>
        public byte VK => (byte)rawValue;

        /// <summary>
        /// Associated Scan Code of a RawKey
        /// </summary>
        public ushort SC => (ushort)(rawValue >> 8);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RawKey (byte vk, ushort sc) => rawValue = (uint)(vk + (sc << 8));


        public override bool Equals (object obj) => obj != null && GetType() == obj.GetType() && Equals((RawKey)obj);
        public bool Equals (RawKey other) => rawValue == other.rawValue;
        public override int GetHashCode () => rawValue.GetHashCode();
        public static bool operator == (RawKey k1, RawKey k2) => k1.rawValue == k2.rawValue;
        public static bool operator != (RawKey k1, RawKey k2) => !(k1 == k2);


        public override string ToString () => ToString(null, null);
        public string ToString (string format) => ToString(format, null);
        public string ToString (IFormatProvider formatProvider) => ToString(null, formatProvider);
        public string ToString (string format, IFormatProvider formatProvider)
        {
            // Format provider is irrelevant, as this will only return InvariantCulture, ignore

            // Check if format is provided, set to default "G" otherwise
            if (string.IsNullOrEmpty(format))
                format = "G";

            // Attempt to retrieve AllKeys value, save value to "name"
            bool parsed = AllKeys.TryGetValue(this, out string name);

            // Begin switchcases for formatting
            switch (format.ToUpperInvariant())
            {
                case "R": // Raw
                    // Retrieve raw data no matter what
                    return RawString();
                case "G": // General
                    // If name was successfully parsed, return the name; otherwise, return raw string
                    return parsed ? name : RawString();
                case "V": // Verbose
                    // Return both name and raw string
                    return (parsed ? name + ", " : string.Empty) + RawString();
                default: // Unsupported format
                    throw new FormatException($"The \"{format}\" format specifier is invalid.");
            }
        }
        /// <summary>
        /// Returns either "special key" format or virtual key byte, depending on if running low-level
        /// </summary>
        public string RawString () => RawInput.WorkInBackground ? $"vk{VK:X2}sc{SC:X3}" : $"0x{VK:X2}";


        /// <summary>
        /// Create a RawKey from standard hook (only handle virtual keys)
        /// </summary>
        public static explicit operator RawKey (IntPtr i) => FromVirtualKey((byte)i);

        /// <summary>
        /// Create a RawKey from a low-level hook
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator RawKey (KeyboardArgs k)
        {
            // Largely lifted from AutoHotkey's "hook.cpp" implementation

            // Begin by casting codepoint uints "Code" and "ScanCode" to byte and ushort respectively
            byte vk = (byte)k.Code;
            ushort sc = (ushort)k.ScanCode;

            // Check if "Extended" flag is raised; if so, increment scancode by 0x100
            if (k.Flags.HasFlag(KeyboardFlags.Extended))
                sc += 0x100;

            // Cast values to RawKey
            return new RawKey(vk, sc);
        }
    }
}
