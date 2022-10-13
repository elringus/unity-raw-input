using System.Collections.Generic;

namespace UnityRawInput
{
    public partial struct RawKey
    {
        public static IReadOnlyDictionary<ushort, RawKey> MatchScanCode { get; } = new Dictionary<ushort, RawKey>()
        {
            {0x001, Escape},
            {0x002, N1},
            {0x003, N2},
            {0x004, N3},
            {0x005, N4},
            {0x006, N5},
            {0x007, N6},
            {0x008, N7},
            {0x009, N8},
            {0x00A, N9},
            {0x00B, N0},
            {0x00C, OEMMinus},
            {0x00D, OEMPlus},
            {0x00E, Back},
            {0x00F, Tab},
            {0x010, Q},
            {0x011, W},
            {0x012, E},
            {0x013, R},
            {0x014, T},
            {0x015, Y},
            {0x016, U},
            {0x017, I},
            {0x018, O},
            {0x019, P},
            {0x01A, OEM4},
            {0x01B, OEM6},
            {0x01C, Return},
            {0x01D, LeftControl},
            {0x01E, A},
            {0x01F, S},
            {0x020, D},
            {0x021, F},
            {0x022, G},
            {0x023, H},
            {0x024, J},
            {0x025, K},
            {0x026, L},
            {0x027, OEM1},
            {0x028, OEM7},
            {0x029, OEM3},
            {0x02A, LeftShift},
            {0x02B, OEM5},
            {0x02C, Z},
            {0x02D, X},
            {0x02E, C},
            {0x02F, V},
            {0x030, B},
            {0x031, N},
            {0x032, M},
            {0x033, OEMComma},
            {0x034, OEMPeriod},
            {0x035, OEM2},
            {0x037, Multiply},
            {0x038, LeftMenu},
            {0x039, Space},
            {0x03A, CapsLock},
            {0x03B, F1},
            {0x03C, F2},
            {0x03D, F3},
            {0x03E, F4},
            {0x03F, F5},
            {0x040, F6},
            {0x041, F7},
            {0x042, F8},
            {0x043, F9},
            {0x044, F10},
            {0x045, Pause},
            {0x046, ScrollLock},
            {0x047, Numpad7},
            {0x048, Numpad8},
            {0x049, Numpad9},
            {0x04A, Subtract},
            {0x04B, Numpad4},
            {0x04C, Numpad5},
            {0x04D, Numpad6},
            {0x04E, Add},
            {0x04F, Numpad1},
            {0x050, Numpad2},
            {0x051, Numpad3},
            {0x052, Numpad0},
            {0x053, Decimal},
            {0x056, OEM102},
            {0x057, F11},
            {0x058, F12},
            {0x05A, OEMWSCtrl},
            {0x05B, OEMFinish},
            {0x05C, OEMJump},
            {0x05D, EREOF},
            {0x05E, OEMBackTab},
            {0x05F, Sleep},
            {0x062, Zoom},
            {0x063, Help},
            {0x064, F13},
            {0x065, F14},
            {0x066, F15},
            {0x067, F16},
            {0x068, F17},
            {0x069, F18},
            {0x06A, F19},
            {0x06B, F20},
            {0x06D, F22},
            {0x06E, F23},
            {0x06F, OEMPA3},
            {0x071, OEMReset},
            {0x073, International1},
            {0x076, F24},
            {0x07B, OEMPA1},
            {0x07E, BrazilianComma},
            {0x110, MediaPrevTrack},
            {0x119, MediaNextTrack},
            {0x11D, RightControl},
            {0x120, VolumeMute},
            {0x121, LaunchApplication2},
            {0x122, MediaPlayPause},
            {0x124, MediaStop},
            {0x12E, VolumeDown},
            {0x130, VolumeUp},
            {0x132, BrowserHome},
            {0x135, Divide},
            {0x136, RightShift},
            {0x138, RightMenu},
            {0x145, NumLock},
            {0x146, Cancel},
            {0x15B, LeftWindows},
            {0x15C, RightWindows},
            {0x15D, Application},
            {0x165, BrowserSearch},
            {0x166, BrowserFavorites},
            {0x167, BrowserRefresh},
            {0x168, BrowserStop},
            {0x169, BrowserForward},
            {0x16A, BrowserBack},
            {0x16B, LaunchApplication1},
            {0x16C, LaunchMail},
            {0x16D, LaunchMediaSelect}
        };
    }
}
