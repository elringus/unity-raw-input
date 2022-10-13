using System.Collections.Generic;

namespace UnityRawInput
{
    public partial struct RawKey
    {
        public static IReadOnlyDictionary<byte, RawKey> MatchVirtualKey { get; } = new Dictionary<byte, RawKey>()
        {
            {0x01, LeftButton},
            {0x02, RightButton},
            {0x03, Cancel},
            {0x04, MiddleButton},
            {0x05, ExtraButton1},
            {0x06, ExtraButton2},
            {0x08, Back},
            {0x09, Tab},
            {0x0C, Clear},
            {0x0D, Return},
            {0x10, Shift},
            {0x11, Control},
            {0x12, Menu},
            {0x13, Pause},
            {0x14, CapsLock},
            {0x15, Kana},
            {0x17, Junja},
            {0x18, Final},
            {0x19, Hanja},
            {0x1B, Escape},
            {0x1C, Convert},
            {0x1D, NonConvert},
            {0x1E, Accept},
            {0x1F, ModeChange},
            {0x20, Space},
            {0x21, Prior},
            {0x22, Next},
            {0x23, End},
            {0x24, Home},
            {0x25, Left},
            {0x26, Up},
            {0x27, Right},
            {0x28, Down},
            {0x29, Select},
            {0x2A, Print},
            {0x2B, Execute},
            {0x2C, Snapshot},
            {0x2D, Insert},
            {0x2E, Delete},
            {0x2F, Help},
            {0x30, N0},
            {0x31, N1},
            {0x32, N2},
            {0x33, N3},
            {0x34, N4},
            {0x35, N5},
            {0x36, N6},
            {0x37, N7},
            {0x38, N8},
            {0x39, N9},
            {0x41, A},
            {0x42, B},
            {0x43, C},
            {0x44, D},
            {0x45, E},
            {0x46, F},
            {0x47, G},
            {0x48, H},
            {0x49, I},
            {0x4A, J},
            {0x4B, K},
            {0x4C, L},
            {0x4D, M},
            {0x4E, N},
            {0x4F, O},
            {0x50, P},
            {0x51, Q},
            {0x52, R},
            {0x53, S},
            {0x54, T},
            {0x55, U},
            {0x56, V},
            {0x57, W},
            {0x58, X},
            {0x59, Y},
            {0x5A, Z},
            {0x5B, LeftWindows},
            {0x5C, RightWindows},
            {0x5D, Application},
            {0x5F, Sleep},
            {0x60, Numpad0},
            {0x61, Numpad1},
            {0x62, Numpad2},
            {0x63, Numpad3},
            {0x64, Numpad4},
            {0x65, Numpad5},
            {0x66, Numpad6},
            {0x67, Numpad7},
            {0x68, Numpad8},
            {0x69, Numpad9},
            {0x6A, Multiply},
            {0x6B, Add},
            {0x6C, Separator},
            {0x6D, Subtract},
            {0x6E, Decimal},
            {0x6F, Divide},
            {0x70, F1},
            {0x71, F2},
            {0x72, F3},
            {0x73, F4},
            {0x74, F5},
            {0x75, F6},
            {0x76, F7},
            {0x77, F8},
            {0x78, F9},
            {0x79, F10},
            {0x7A, F11},
            {0x7B, F12},
            {0x7C, F13},
            {0x7D, F14},
            {0x7E, F15},
            {0x7F, F16},
            {0x80, F17},
            {0x81, F18},
            {0x82, F19},
            {0x83, F20},
            {0x84, F21},
            {0x85, F22},
            {0x86, F23},
            {0x87, F24},
            {0x90, NumLock},
            {0x91, ScrollLock},
            {0x92, Fujitsu_Jisho},
            {0x93, Fujitsu_Masshou},
            {0x94, Fujitsu_Touroku},
            {0x95, Fujitsu_Loya},
            {0x96, Fujitsu_Roya},
            {0x9A, LeftButtonAlt},
            {0x9B, RightButtonAlt},
            {0x9C, WheelLeft},
            {0x9D, WheelRight},
            {0x9E, WheelDown},
            {0x9F, WheelUp},
            {0xA0, LeftShift},
            {0xA1, RightShift},
            {0xA2, LeftControl},
            {0xA3, RightControl},
            {0xA4, LeftMenu},
            {0xA5, RightMenu},
            {0xA6, BrowserBack},
            {0xA7, BrowserForward},
            {0xA8, BrowserRefresh},
            {0xA9, BrowserStop},
            {0xAA, BrowserSearch},
            {0xAB, BrowserFavorites},
            {0xAC, BrowserHome},
            {0xAD, VolumeMute},
            {0xAE, VolumeDown},
            {0xAF, VolumeUp},
            {0xB0, MediaNextTrack},
            {0xB1, MediaPrevTrack},
            {0xB2, MediaStop},
            {0xB3, MediaPlayPause},
            {0xB4, LaunchMail},
            {0xB5, LaunchMediaSelect},
            {0xB6, LaunchApplication1},
            {0xB7, LaunchApplication2},
            {0xBA, OEM1},
            {0xBB, OEMPlus},
            {0xBC, OEMComma},
            {0xBD, OEMMinus},
            {0xBE, OEMPeriod},
            {0xBF, OEM2},
            {0xC0, OEM3},
            {0xC1, International1},
            {0xC2, BrazilianComma},
            {0xDB, OEM4},
            {0xDC, OEM5},
            {0xDD, OEM6},
            {0xDE, OEM7},
            {0xDF, OEM8},
            {0xE1, OEMAX},
            {0xE2, OEM102},
            {0xE3, ICOHelp},
            {0xE4, ICO00},
            {0xE5, ProcessKey},
            {0xE6, ICOClear},
            {0xE7, Packet},
            {0xE9, OEMReset},
            {0xEA, OEMJump},
            {0xEB, OEMPA1},
            {0xEC, OEMPA2},
            {0xED, OEMPA3},
            {0xEE, OEMWSCtrl},
            {0xEF, OEMCUSel},
            {0xF0, OEMATTN},
            {0xF1, OEMFinish},
            {0xF2, OEMCopy},
            {0xF3, OEMAuto},
            {0xF4, OEMENLW},
            {0xF5, OEMBackTab},
            {0xF6, ATTN},
            {0xF7, CRSel},
            {0xF8, EXSel},
            {0xF9, EREOF},
            {0xFA, Play},
            {0xFB, Zoom},
            {0xFC, Noname},
            {0xFD, PA1},
            {0xFE, OEMClear}
        };
    }
}
