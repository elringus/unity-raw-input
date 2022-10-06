using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace UnityRawInput
{
    /// <summary>
    /// Specific name attributed to a given <see href="https://docs.google.com/spreadsheets/d/1GSj0gKDxyWAecB3SIyEZ2ssPETZkkxn67gdIwL1zFUs/">keycode</see>
    /// </summary>
    public struct RawKey : IEquatable<RawKey>
    {
        public RawKey(byte vk, ushort sc) { VK = vk; SC = sc; }
        public byte VK { get; private set; }
        public ushort SC { get; private set; }

        public override string ToString() => AllKeys.TryGetValue(this, out string name) ? name
            : SC < 0x100 && AllKeys.TryGetValue(new RawKey(this.VK, (ushort)(this.SC + 0x100)), out name) ? name // TODO: Figure out why Unity doesn't want to get upper byte of scancode sometimes
            : $"vk{VK:X2}sc{SC:X3}";

        public override bool Equals(object obj) => obj != null && GetType() == obj.GetType() && Equals((RawKey)obj);
        public bool Equals(RawKey other) => VK == other.VK && SC == other.SC;
        public override int GetHashCode() => Tuple.Create(VK, SC).GetHashCode();
        public static bool operator ==(RawKey k1, RawKey k2) => k1.VK == k2.VK && k1.SC == k2.SC;
        public static bool operator !=(RawKey k1, RawKey k2) => !(k1 == k2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator RawKey(string s)
        {
            var key = new RawKey();
            var vk = Regex.Match(s, @"vk[\da-f]{2}", RegexOptions.IgnoreCase).ToString();
            var sc = Regex.Match(s, @"sc[\da-f]{3}", RegexOptions.IgnoreCase).ToString();
            if (string.IsNullOrEmpty(vk) && string.IsNullOrEmpty(sc)) return Parse(s);
            if (!string.IsNullOrEmpty(vk))
            {
                try
                {
                    key.VK = byte.Parse(vk.Replace("vk", ""), NumberStyles.HexNumber);
                    if (string.IsNullOrEmpty(sc)) return Parse(key.VK);
                }
                catch (FormatException)
                {
                    UnityEngine.Debug.LogWarning($"Unable to parse '{vk}'");
                }
            }
            if (!string.IsNullOrEmpty(sc))
            {
                try
                {
                    key.SC = ushort.Parse(sc.Replace("sc", ""), NumberStyles.HexNumber);
                    if (string.IsNullOrEmpty(vk)) return Parse(key.SC);
                }
                catch (FormatException)
                {
                    UnityEngine.Debug.LogWarning($"Unable to parse '{sc}'");
                }
            }
            return key;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator RawKey(KeyboardArgs k)
        {
            return new RawKey((byte)k.Code, (ushort)k.ScanCode);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator RawKey(IntPtr i)
        {
            return Parse((byte)i);
        }

        public static RawKey Parse(string parse)
        {
            switch (parse.ToLowerInvariant())
            {
                case "leftbutton":
                case "leftclick":
                case "mouse1":
                case "m1":
                    return LeftButton;
                case "rightbutton":
                case "rightclick":
                case "mouse2":
                case "m2":
                    return RightButton;
                case "ctrlbreak":
                case "cancel":
                    return CtrlBreak;
                case "middlebutton":
                case "middleclick":
                case "mouse3":
                case "m3":
                    return MiddleButton;
                case "extrabutton1":
                case "xbutton1":
                case "xb1":
                case "mouse4":
                case "m4":
                    return XButton1;
                case "extrabutton2":
                case "xbutton2":
                case "xb2":
                case "mouse5":
                case "m5":
                    return XButton2;
                case "backspace":
                case "back":
                    return Backspace;
                case "tab":
                    return Tab;
                case "numpadclear":
                case "clear":
                    return NumpadClear;
                case "return":
                case "enter":
                    return Enter;
                case "shift":
                    return Shift;
                case "ctrl":
                case "control":
                    return Control;
                case "menu":
                case "alt":
                    return Alt;
                case "pause":
                    return Pause;
                case "capslock":
                    return CapsLock;
                case "kana":
                case "hangeul":
                case "hangul":
                    return Kana;
                case "junja":
                    return Junja;
                case "final":
                    return Final;
                case "hanja":
                case "kanji":
                    return Hanja;
                case "escape":
                case "esc":
                    return Escape;
                case "convert":
                    return Convert;
                case "nonconvert":
                    return NonConvert;
                case "accept":
                    return Accept;
                case "modechange":
                case "change":
                    return ModeChange;
                case "space":
                    return Space;
                case "numpadpgup":
                case "prior":
                    return PageUp;
                case "next":
                case "numpadpagedown":
                case "numpgdn":
                    return PageDown;
                case "end":
                case "numpadend":
                case "numend":
                    return End;
                case "home":
                case "numpadhome":
                case "numhome":
                    return Home;
                case "left":
                case "numpadleft":
                case "numleft":
                    return Left;
                case "up":
                case "numpadup":
                case "numup":
                    return Up;
                case "right":
                case "numpadright":
                case "numright":
                    return Right;
                case "down":
                case "numpaddown":
                case "numdown":
                    return Down;
                case "select":
                    return Select;
                case "print":
                    return Print;
                case "execute":
                    return Execute;
                case "printscreen":
                    return PrintScreen;
                case "numpadinsert":
                case "numinstert":
                case "numins":
                case "insert":
                    return Insert;
                case "numpaddelete":
                case "numpaddel":
                case "numdel":
                case "delete":
                    return Delete;
                case "help":
                    return Help;
                case "0":
                case "n0":
                    return N0;
                case "1":
                case "n1":
                    return N1;
                case "2":
                case "n2":
                    return N2;
                case "3":
                case "n3":
                    return N3;
                case "4":
                case "n4":
                    return N4;
                case "5":
                case "n5":
                    return N5;
                case "6":
                case "n6":
                    return N6;
                case "7":
                case "n7":
                    return N7;
                case "8":
                case "n8":
                    return N8;
                case "9":
                case "n9":
                    return N9;
                case "a":
                    return A;
                case "b":
                    return B;
                case "c":
                    return C;
                case "d":
                    return D;
                case "e":
                    return E;
                case "f":
                    return F;
                case "g":
                    return G;
                case "h":
                    return H;
                case "i":
                    return I;
                case "j":
                    return J;
                case "k":
                    return K;
                case "l":
                    return L;
                case "m":
                    return M;
                case "n":
                    return N;
                case "o":
                    return O;
                case "p":
                    return P;
                case "q":
                    return Q;
                case "r":
                    return R;
                case "s":
                    return S;
                case "t":
                    return T;
                case "u":
                    return U;
                case "v":
                    return V;
                case "w":
                    return W;
                case "x":
                    return X;
                case "y":
                    return Y;
                case "z":
                    return Z;
                case "leftwindows":
                case "lwin":
                case "leftos":
                case "los":
                    return LeftWindows;
                case "rightwindows":
                case "rwin":
                case "rightos":
                case "ros":
                    return RightWindows;
                case "appskey":
                case "apps":
                case "application":
                    return AppsKey;
                case "sleep":
                    return Sleep;
                case "num0":
                case "numpad0":
                    return Numpad0;
                case "num1":
                case "numpad1":
                    return Numpad1;
                case "num2":
                case "numpad2":
                    return Numpad2;
                case "num3":
                case "numpad3":
                    return Numpad3;
                case "num4":
                case "numpad4":
                    return Numpad4;
                case "num5":
                case "numpad5":
                    return Numpad5;
                case "num6":
                case "numpad6":
                    return Numpad6;
                case "num":
                case "numpad7":
                    return Numpad7;
                case "num8":
                case "numpad8":
                    return Numpad8;
                case "num9":
                case "numpad9":
                    return Numpad9;
                case "mult":
                case "multiply":
                case "nummult":
                case "numpadmultiply":
                    return NumpadMultiply;
                case "add":
                case "numadd":
                case "numpadadd":
                    return NumpadAdd;
                case "separator":
                    return Separator;
                case "sub":
                case "subtract":
                case "numsub":
                case "numpadsubtract":
                    return NumpadSubtract;
                case "dec":
                case "decimal":
                case "numpaddecimal":
                case "numdec":
                case "numpaddot":
                case "numdot":
                    return NumpadDot;
                case "div":
                case "divide":
                case "numdiv":
                case "numpaddivide":
                    return NumpadDivide;
                case "f1":
                    return F1;
                case "f2":
                    return F2;
                case "f3":
                    return F3;
                case "f4":
                    return F4;
                case "f5":
                    return F5;
                case "f6":
                    return F6;
                case "f7":
                    return F7;
                case "f8":
                    return F8;
                case "f9":
                    return F9;
                case "f10":
                    return F10;
                case "f11":
                    return F11;
                case "f12":
                    return F12;
                case "f13":
                    return F13;
                case "f14":
                    return F14;
                case "f15":
                    return F15;
                case "f16":
                    return F16;
                case "f17":
                    return F17;
                case "f18":
                    return F18;
                case "f19":
                    return F19;
                case "f20":
                    return F20;
                case "f21":
                    return F21;
                case "f22":
                    return F22;
                case "f23":
                    return F23;
                case "f24":
                    return F24;
                case "numpadlock":
                case "numlock":
                    return Numlock;
                case "scrolllock":
                    return ScrollLock;
                case "jisho":
                case "fujitsujisho":
                case "necequal":
                    return Jisho;
                case "mashu":
                case "masshou":
                case "fujitsumasshou":
                    return Masshou;
                case "touroku":
                case "fujitsutouroku":
                    return Touroku;
                case "loya":
                case "fujitsuloya":
                    return Loya;
                case "roya":
                case "fujitsuroya":
                    return Roya;
                case "lclickalt":
                case "leftclickalt":
                case "lbuttonalt":
                case "leftbuttonalt":
                    return LeftButtonAlt;
                case "rclickalt":
                case "rightclickalt":
                case "rbuttonalt":
                case "rightbuttonalt":
                    return RightButtonAlt;
                case "wheelleft":
                    return WheelLeft;
                case "wheelright":
                    return WheelRight;
                case "wheeldown":
                    return WheelDown;
                case "wheelup":
                    return WheelUp;
                case "lshift":
                case "leftshift":
                    return LeftShift;
                case "rshift":
                case "rightshift":
                    return RightShift;
                case "lcontrol":
                case "leftcontrol":
                    return LeftControl;
                case "rcontrol":
                case "rightcontrol":
                    return RightControl;
                case "lmenu":
                case "leftmenu":
                case "lalt":
                case "leftalt":
                    return LeftAlt;
                case "rmenu":
                case "rightmenu":
                case "ralt":
                case "rightalt":
                    return RightAlt;
                case "browserback":
                    return BrowserBack;
                case "browserforward":
                    return BrowserForward;
                case "browserrefresh":
                    return BrowserRefresh;
                case "browserstop":
                    return BrowserStop;
                case "browsersearch":
                    return BrowserSearch;
                case "browserfavorites":
                    return BrowserFavorites;
                case "browserhome":
                    return BrowserHome;
                case "volumemute":
                    return VolumeMute;
                case "volumedown":
                    return VolumeDown;
                case "volumeup":
                    return VolumeUp;
                case "medianext":
                case "medianexttrack":
                    return MediaNext;
                case "mediaprev":
                case "mediaprevtrack":
                    return MediaPrev;
                case "mediastop":
                    return MediaStop;
                case "mediaplay":
                case "mediapause":
                case "mediaplaypause":
                    return MediaPlayPause;
                case "mail":
                case "launchmail":
                    return LaunchMail;
                case "media":
                case "launchmedia":
                    return LaunchMedia;
                case "app1":
                case "launchapp1":
                    return LaunchApp1;
                case "app2":
                case "launchapp2":
                    return LaunchApp2;
                case "oem1":
                    return OEM1;
                case "oemplus":
                    return OEMPlus;
                case "oemcomma":
                    return OEMComma;
                case "oemminus":
                case "minus":
                    return OEMMinus;
                case "oemperiod":
                    return OEMPeriod;
                case "oem2":
                    return OEM2;
                case "oem3":
                    return OEM3;
                case "international1":
                    return International1;
                case "numpadcomma":
                case "braziliancomma":
                    return BrazilianComma;
                case "oem4":
                    return OEM4;
                case "oem5":
                    return OEM5;
                case "oem6":
                    return OEM6;
                case "oem7":
                    return OEM7;
                case "oem8":
                    return OEM8;
                case "oem102":
                    return OEM102;
                case "processkey":
                    return ProcessKey;
                case "packet":
                    return Packet;
                case "oemreset":
                    return OEMReset;
                case "oemjump":
                    return OEMJump;
                case "international5":
                case "intl5":
                case "oempa1":
                    return International5;
                case "oempa3":
                    return OEMPa3;
                case "oemwsctrl":
                    return OEMWsCtrl;
                case "oemfinish":
                    return OEMFinish;
                case "oemauto":
                    return OEMAuto;
                case "oembacktab":
                    return OEMBackTab;
                case "attn":
                    return Attn;
                case "crsel":
                    return CRSel;
                case "exsel":
                    return EXSel;
                case "ereof":
                    return EREOF;
                case "play":
                    return Play;
                case "zoom":
                    return Zoom;
                case "noname":
                    return Noname;
                case "pa1":
                    return PA1;
                case "oemclear":
                    return OEMClear;
                case "international2":
                case "intl2":
                    return International2;
                case "international4":
                case "intl4":
                    return International4;
                case "international3":
                case "intl3":
                    return International3;
                case "oemmax":
                case "max":
                    return OEMMax;
                case "icohelp":
                    return IcoHelp;
                case "ico00":
                case "00":
                    return Ico00;
                case "icoclear":
                    return IcoClear;
                case "oempa2":
                    return OEMPa2;
                case "oemcusel":
                    return OEMCUSel;
                case "oemattn":
                    return OEMAttn;
                case "oemenlw":
                    return OEMENLW;
                case "oemcopy":
                    return OEMCopy;
            }
            return new RawKey();
        }

        public static RawKey Parse(byte parse)
        {
            switch (parse)
            {
                case 0x01: return LeftButton;
                case 0x02: return RightButton;
                case 0x03: return CtrlBreak;
                case 0x04: return MiddleButton;
                case 0x05: return XButton1;
                case 0x06: return XButton2;
                case 0x08: return Backspace;
                case 0x09: return Tab;
                case 0x0C: return NumpadClear;
                case 0x0D: return Enter;
                case 0x10: return Shift;
                case 0x11: return Control;
                case 0x12: return Alt;
                case 0x13: return Pause;
                case 0x14: return CapsLock;
                case 0x15: return Kana;
                case 0x17: return Junja;
                case 0x18: return Final;
                case 0x19: return Hanja;
                case 0x1B: return Escape;
                case 0x1C: return Convert;
                case 0x1D: return NonConvert;
                case 0x1E: return Accept;
                case 0x1F: return ModeChange;
                case 0x20: return Space;
                case 0x21: return PageUp;
                case 0x22: return PageDown;
                case 0x23: return End;
                case 0x24: return Home;
                case 0x25: return Left;
                case 0x26: return Up;
                case 0x27: return Right;
                case 0x28: return Down;
                case 0x29: return Select;
                case 0x2A: return Print;
                case 0x2B: return Execute;
                case 0x2C: return PrintScreen;
                case 0x2D: return Insert;
                case 0x2E: return Delete;
                case 0x2F: return Help;
                case 0x30: return N0;
                case 0x31: return N1;
                case 0x32: return N2;
                case 0x33: return N3;
                case 0x34: return N4;
                case 0x35: return N5;
                case 0x36: return N6;
                case 0x37: return N7;
                case 0x38: return N8;
                case 0x39: return N9;
                case 0x41: return A;
                case 0x42: return B;
                case 0x43: return C;
                case 0x44: return D;
                case 0x45: return E;
                case 0x46: return F;
                case 0x47: return G;
                case 0x48: return H;
                case 0x49: return I;
                case 0x4A: return J;
                case 0x4B: return K;
                case 0x4C: return L;
                case 0x4D: return M;
                case 0x4E: return N;
                case 0x4F: return O;
                case 0x50: return P;
                case 0x51: return Q;
                case 0x52: return R;
                case 0x53: return S;
                case 0x54: return T;
                case 0x55: return U;
                case 0x56: return V;
                case 0x57: return W;
                case 0x58: return X;
                case 0x59: return Y;
                case 0x5A: return Z;
                case 0x5B: return LeftWindows;
                case 0x5C: return RightWindows;
                case 0x5D: return AppsKey;
                case 0x5F: return Sleep;
                case 0x60: return Numpad0;
                case 0x61: return Numpad1;
                case 0x62: return Numpad2;
                case 0x63: return Numpad3;
                case 0x64: return Numpad4;
                case 0x65: return Numpad5;
                case 0x66: return Numpad6;
                case 0x67: return Numpad7;
                case 0x68: return Numpad8;
                case 0x69: return Numpad9;
                case 0x6A: return NumpadMultiply;
                case 0x6B: return NumpadAdd;
                case 0x6C: return Separator;
                case 0x6D: return NumpadSubtract;
                case 0x6E: return NumpadDot;
                case 0x6F: return NumpadDivide;
                case 0x70: return F1;
                case 0x71: return F2;
                case 0x72: return F3;
                case 0x73: return F4;
                case 0x74: return F5;
                case 0x75: return F6;
                case 0x76: return F7;
                case 0x77: return F8;
                case 0x78: return F9;
                case 0x79: return F10;
                case 0x7A: return F11;
                case 0x7B: return F12;
                case 0x7C: return F13;
                case 0x7D: return F14;
                case 0x7E: return F15;
                case 0x7F: return F16;
                case 0x80: return F17;
                case 0x81: return F18;
                case 0x82: return F19;
                case 0x83: return F20;
                case 0x84: return F21;
                case 0x85: return F22;
                case 0x86: return F23;
                case 0x87: return F24;
                case 0x90: return Numlock;
                case 0x91: return ScrollLock;
                case 0x92: return Jisho;
                case 0x93: return Masshou;
                case 0x94: return Touroku;
                case 0x95: return Loya;
                case 0x96: return Roya;
                case 0x9A: return LeftButtonAlt;
                case 0x9B: return RightButtonAlt;
                case 0x9C: return WheelLeft;
                case 0x9D: return WheelRight;
                case 0x9E: return WheelDown;
                case 0x9F: return WheelUp;
                case 0xA0: return LeftShift;
                case 0xA1: return RightShift;
                case 0xA2: return LeftControl;
                case 0xA3: return RightControl;
                case 0xA4: return LeftAlt;
                case 0xA5: return RightAlt;
                case 0xA6: return BrowserBack;
                case 0xA7: return BrowserForward;
                case 0xA8: return BrowserRefresh;
                case 0xA9: return BrowserStop;
                case 0xAA: return BrowserSearch;
                case 0xAB: return BrowserFavorites;
                case 0xAC: return BrowserHome;
                case 0xAD: return VolumeMute;
                case 0xAE: return VolumeDown;
                case 0xAF: return VolumeUp;
                case 0xB0: return MediaNext;
                case 0xB1: return MediaPrev;
                case 0xB2: return MediaStop;
                case 0xB3: return MediaPlayPause;
                case 0xB4: return LaunchMail;
                case 0xB5: return LaunchMedia;
                case 0xB6: return LaunchApp1;
                case 0xB7: return LaunchApp2;
                case 0xBA: return OEM1;
                case 0xBB: return OEMPlus;
                case 0xBC: return OEMComma;
                case 0xBD: return OEMMinus;
                case 0xBE: return OEMPeriod;
                case 0xBF: return OEM2;
                case 0xC0: return OEM3;
                case 0xC1: return International1;
                case 0xC2: return BrazilianComma;
                case 0xDB: return OEM4;
                case 0xDC: return OEM5;
                case 0xDD: return OEM6;
                case 0xDE: return OEM7;
                case 0xDF: return OEM8;
                case 0xE1: return OEMMax;
                case 0xE2: return OEM102;
                case 0xE3: return IcoHelp;
                case 0xE4: return Ico00;
                case 0xE5: return ProcessKey;
                case 0xE6: return IcoClear;
                case 0xE7: return Packet;
                case 0xE9: return OEMReset;
                case 0xEA: return OEMJump;
                case 0xEB: return International5;
                case 0xED: return OEMPa3;
                case 0xEE: return OEMWsCtrl;
                case 0xEF: return OEMCUSel;
                case 0xF0: return OEMAttn;
                case 0xF1: return OEMFinish;
                case 0xF2: return OEMCopy;
                case 0xF3: return OEMAuto;
                case 0xF4: return OEMENLW;
                case 0xF5: return OEMBackTab;
                case 0xF6: return Attn;
                case 0xF7: return CRSel;
                case 0xF8: return EXSel;
                case 0xF9: return EREOF;
                case 0xFA: return Play;
                case 0xFB: return Zoom;
                case 0xFC: return Noname;
                case 0xFD: return PA1;
                case 0xFE: return OEMClear;
                default: return new RawKey(parse, 0);
            }
        }

        public static RawKey Parse(ushort parse)
        {
            switch (parse)
            {
                case 0x001: return Escape;
                case 0x002: return N1;
                case 0x003: return N2;
                case 0x004: return N3;
                case 0x005: return N4;
                case 0x006: return N5;
                case 0x007: return N6;
                case 0x008: return N7;
                case 0x009: return N8;
                case 0x00A: return N9;
                case 0x00B: return N0;
                case 0x00C: return OEMMinus;
                case 0x00D: return OEMPlus;
                case 0x00E: return Backspace;
                case 0x00F: return Tab;
                case 0x010: return Q;
                case 0x011: return W;
                case 0x012: return E;
                case 0x013: return R;
                case 0x014: return T;
                case 0x015: return Y;
                case 0x016: return U;
                case 0x017: return I;
                case 0x018: return O;
                case 0x019: return P;
                case 0x01A: return OEM4;
                case 0x01B: return OEM6;
                case 0x01C: return Enter;
                case 0x01D: return LeftControl;
                case 0x01E: return A;
                case 0x01F: return S;
                case 0x020: return D;
                case 0x021: return F;
                case 0x022: return G;
                case 0x023: return H;
                case 0x024: return J;
                case 0x025: return K;
                case 0x026: return L;
                case 0x027: return OEM1;
                case 0x028: return OEM7;
                case 0x029: return OEM3;
                case 0x02A: return LeftShift;
                case 0x02B: return OEM5;
                case 0x02C: return Z;
                case 0x02D: return X;
                case 0x02E: return C;
                case 0x02F: return V;
                case 0x030: return B;
                case 0x031: return N;
                case 0x032: return M;
                case 0x033: return OEMComma;
                case 0x034: return OEMPeriod;
                case 0x035: return OEM2;
                case 0x037: return NumpadMultiply;
                case 0x038: return LeftAlt;
                case 0x039: return Space;
                case 0x03A: return CapsLock;
                case 0x03B: return F1;
                case 0x03C: return F2;
                case 0x03D: return F3;
                case 0x03E: return F4;
                case 0x03F: return F5;
                case 0x040: return F6;
                case 0x041: return F7;
                case 0x042: return F8;
                case 0x043: return F9;
                case 0x044: return F10;
                case 0x045: return Pause;
                case 0x046: return ScrollLock;
                case 0x047: return Numpad7;
                case 0x048: return Numpad8;
                case 0x049: return Numpad9;
                case 0x04A: return NumpadSubtract;
                case 0x04B: return Numpad4;
                case 0x04C: return Numpad5;
                case 0x04D: return Numpad6;
                case 0x04E: return NumpadAdd;
                case 0x04F: return Numpad1;
                case 0x050: return Numpad2;
                case 0x051: return Numpad3;
                case 0x052: return Numpad0;
                case 0x053: return NumpadDot;
                case 0x056: return OEM102;
                case 0x057: return F11;
                case 0x058: return F12;
                case 0x05A: return OEMWsCtrl;
                case 0x05B: return OEMFinish;
                case 0x05C: return OEMJump;
                case 0x05D: return EREOF;
                case 0x05E: return OEMBackTab;
                case 0x05F: return Sleep;
                case 0x062: return Zoom;
                case 0x063: return Help;
                case 0x064: return F13;
                case 0x065: return F14;
                case 0x066: return F15;
                case 0x067: return F16;
                case 0x068: return F17;
                case 0x069: return F18;
                case 0x06A: return F19;
                case 0x06B: return F20;
                case 0x06D: return F22;
                case 0x06E: return F23;
                case 0x06F: return OEMPa3;
                case 0x071: return OEMReset;
                case 0x073: return International1;
                case 0x076: return F24;
                case 0x07B: return International5;
                case 0x07E: return BrazilianComma;
                case 0x110: return MediaPrev;
                case 0x119: return MediaNext;
                case 0x11D: return RightControl;
                case 0x120: return VolumeMute;
                case 0x121: return LaunchApp2;
                case 0x122: return MediaPlayPause;
                case 0x124: return MediaStop;
                case 0x12E: return VolumeDown;
                case 0x130: return VolumeUp;
                case 0x132: return BrowserHome;
                case 0x135: return NumpadDivide;
                case 0x136: return RightShift;
                case 0x138: return RightAlt;
                case 0x145: return Numlock;
                case 0x146: return CtrlBreak;
                case 0x15B: return LeftWindows;
                case 0x15C: return RightWindows;
                case 0x15D: return AppsKey;
                case 0x165: return BrowserSearch;
                case 0x166: return BrowserFavorites;
                case 0x167: return BrowserRefresh;
                case 0x168: return BrowserStop;
                case 0x169: return BrowserForward;
                case 0x16A: return BrowserBack;
                case 0x16B: return LaunchApp1;
                case 0x16C: return LaunchMail;
                case 0x16D: return LaunchMedia;
                default: return new RawKey(0, parse);
            }
        }

        public static RawKey LeftButton => new RawKey(0x01, 0x000);
        public static RawKey RightButton => new RawKey(0x02, 0x000);
        public static RawKey CtrlBreak => new RawKey(0x03, 0x146);
        public static RawKey MiddleButton => new RawKey(0x04, 0x000);
        public static RawKey XButton1 => new RawKey(0x05, 0x000);
        public static RawKey XButton2 => new RawKey(0x06, 0x000);
        public static RawKey Backspace => new RawKey(0x08, 0x00E);
        public static RawKey Tab => new RawKey(0x09, 0x00F);
        public static RawKey NumpadClear => new RawKey(0x0C, 0x04C);
        public static RawKey Enter => new RawKey(0x0D, 0x01C);
        public static RawKey Shift => new RawKey(0x10, 0x02A);
        public static RawKey Control => new RawKey(0x11, 0x01D);
        public static RawKey Alt => new RawKey(0x12, 0x038);
        public static RawKey Pause => new RawKey(0x13, 0x045);
        public static RawKey CapsLock => new RawKey(0x14, 0x03A);
        public static RawKey Kana => new RawKey(0x15, 0x000);
        public static RawKey Junja => new RawKey(0x17, 0x000);
        public static RawKey Final => new RawKey(0x18, 0x000);
        public static RawKey Hanja => new RawKey(0x19, 0x000);
        public static RawKey Escape => new RawKey(0x1B, 0x001);
        public static RawKey Convert => new RawKey(0x1C, 0x000);
        public static RawKey NonConvert => new RawKey(0x1D, 0x000);
        public static RawKey Accept => new RawKey(0x1E, 0x000);
        public static RawKey ModeChange => new RawKey(0x1F, 0x000);
        public static RawKey Space => new RawKey(0x20, 0x039);
        public static RawKey PageUp => new RawKey(0x21, 0x049);
        public static RawKey PageDown => new RawKey(0x22, 0x051);
        public static RawKey End => new RawKey(0x23, 0x04F);
        public static RawKey Home => new RawKey(0x24, 0x047);
        public static RawKey Left => new RawKey(0x25, 0x04B);
        public static RawKey Up => new RawKey(0x26, 0x048);
        public static RawKey Right => new RawKey(0x27, 0x04D);
        public static RawKey Down => new RawKey(0x28, 0x050);
        public static RawKey Select => new RawKey(0x29, 0x000);
        public static RawKey Print => new RawKey(0x2A, 0x000);
        public static RawKey Execute => new RawKey(0x2B, 0x000);
        public static RawKey PrintScreen => new RawKey(0x2C, 0x154);
        public static RawKey Insert => new RawKey(0x2D, 0x052);
        public static RawKey Delete => new RawKey(0x2E, 0x053);
        public static RawKey Help => new RawKey(0x2F, 0x063);
        public static RawKey N0 => new RawKey(0x30, 0x00B);
        public static RawKey N1 => new RawKey(0x31, 0x002);
        public static RawKey N2 => new RawKey(0x32, 0x003);
        public static RawKey N3 => new RawKey(0x33, 0x004);
        public static RawKey N4 => new RawKey(0x34, 0x005);
        public static RawKey N5 => new RawKey(0x35, 0x006);
        public static RawKey N6 => new RawKey(0x36, 0x007);
        public static RawKey N7 => new RawKey(0x37, 0x008);
        public static RawKey N8 => new RawKey(0x38, 0x009);
        public static RawKey N9 => new RawKey(0x39, 0x00A);
        public static RawKey A => new RawKey(0x41, 0x01E);
        public static RawKey B => new RawKey(0x42, 0x030);
        public static RawKey C => new RawKey(0x43, 0x02E);
        public static RawKey D => new RawKey(0x44, 0x020);
        public static RawKey E => new RawKey(0x45, 0x012);
        public static RawKey F => new RawKey(0x46, 0x021);
        public static RawKey G => new RawKey(0x47, 0x022);
        public static RawKey H => new RawKey(0x48, 0x023);
        public static RawKey I => new RawKey(0x49, 0x017);
        public static RawKey J => new RawKey(0x4A, 0x024);
        public static RawKey K => new RawKey(0x4B, 0x025);
        public static RawKey L => new RawKey(0x4C, 0x026);
        public static RawKey M => new RawKey(0x4D, 0x032);
        public static RawKey N => new RawKey(0x4E, 0x031);
        public static RawKey O => new RawKey(0x4F, 0x018);
        public static RawKey P => new RawKey(0x50, 0x019);
        public static RawKey Q => new RawKey(0x51, 0x010);
        public static RawKey R => new RawKey(0x52, 0x013);
        public static RawKey S => new RawKey(0x53, 0x01F);
        public static RawKey T => new RawKey(0x54, 0x014);
        public static RawKey U => new RawKey(0x55, 0x016);
        public static RawKey V => new RawKey(0x56, 0x02F);
        public static RawKey W => new RawKey(0x57, 0x011);
        public static RawKey X => new RawKey(0x58, 0x02D);
        public static RawKey Y => new RawKey(0x59, 0x015);
        public static RawKey Z => new RawKey(0x5A, 0x02C);
        public static RawKey LeftWindows => new RawKey(0x5B, 0x15B);
        public static RawKey RightWindows => new RawKey(0x5C, 0x15C);
        public static RawKey AppsKey => new RawKey(0x5D, 0x15D);
        public static RawKey Sleep => new RawKey(0x5F, 0x05F);
        public static RawKey Numpad0 => new RawKey(0x60, 0x052);
        public static RawKey Numpad1 => new RawKey(0x61, 0x04F);
        public static RawKey Numpad2 => new RawKey(0x62, 0x050);
        public static RawKey Numpad3 => new RawKey(0x63, 0x051);
        public static RawKey Numpad4 => new RawKey(0x64, 0x04B);
        public static RawKey Numpad5 => new RawKey(0x65, 0x04C);
        public static RawKey Numpad6 => new RawKey(0x66, 0x04D);
        public static RawKey Numpad7 => new RawKey(0x67, 0x047);
        public static RawKey Numpad8 => new RawKey(0x68, 0x048);
        public static RawKey Numpad9 => new RawKey(0x69, 0x049);
        public static RawKey NumpadMultiply => new RawKey(0x6A, 0x037);
        public static RawKey NumpadAdd => new RawKey(0x6B, 0x04E);
        public static RawKey Separator => new RawKey(0x6C, 0x000);
        public static RawKey NumpadSubtract => new RawKey(0x6D, 0x04A);
        public static RawKey NumpadDot => new RawKey(0x6E, 0x053);
        public static RawKey NumpadDivide => new RawKey(0x6F, 0x135);
        public static RawKey F1 => new RawKey(0x70, 0x03B);
        public static RawKey F2 => new RawKey(0x71, 0x03C);
        public static RawKey F3 => new RawKey(0x72, 0x03D);
        public static RawKey F4 => new RawKey(0x73, 0x03E);
        public static RawKey F5 => new RawKey(0x74, 0x03F);
        public static RawKey F6 => new RawKey(0x75, 0x040);
        public static RawKey F7 => new RawKey(0x76, 0x041);
        public static RawKey F8 => new RawKey(0x77, 0x042);
        public static RawKey F9 => new RawKey(0x78, 0x043);
        public static RawKey F10 => new RawKey(0x79, 0x044);
        public static RawKey F11 => new RawKey(0x7A, 0x057);
        public static RawKey F12 => new RawKey(0x7B, 0x058);
        public static RawKey F13 => new RawKey(0x7C, 0x064);
        public static RawKey F14 => new RawKey(0x7D, 0x065);
        public static RawKey F15 => new RawKey(0x7E, 0x066);
        public static RawKey F16 => new RawKey(0x7F, 0x067);
        public static RawKey F17 => new RawKey(0x80, 0x068);
        public static RawKey F18 => new RawKey(0x81, 0x069);
        public static RawKey F19 => new RawKey(0x82, 0x06A);
        public static RawKey F20 => new RawKey(0x83, 0x06B);
        public static RawKey F21 => new RawKey(0x84, 0x06B);
        public static RawKey F22 => new RawKey(0x85, 0x06D);
        public static RawKey F23 => new RawKey(0x86, 0x06E);
        public static RawKey F24 => new RawKey(0x87, 0x076);
        public static RawKey Numlock => new RawKey(0x90, 0x145);
        public static RawKey ScrollLock => new RawKey(0x91, 0x046);
        public static RawKey Jisho => new RawKey(0x92, 0x000);
        public static RawKey Masshou => new RawKey(0x93, 0x000);
        public static RawKey Touroku => new RawKey(0x94, 0x000);
        public static RawKey Loya => new RawKey(0x95, 0x000);
        public static RawKey Roya => new RawKey(0x96, 0x000);
        public static RawKey LeftButtonAlt => new RawKey(0x9A, 0x000);
        public static RawKey RightButtonAlt => new RawKey(0x9B, 0x000);
        public static RawKey WheelLeft => new RawKey(0x9C, 0x001);
        public static RawKey WheelRight => new RawKey(0x9D, 0x001);
        public static RawKey WheelDown => new RawKey(0x9E, 0x001);
        public static RawKey WheelUp => new RawKey(0x9F, 0x001);
        public static RawKey LeftShift => new RawKey(0xA0, 0x02A);
        public static RawKey RightShift => new RawKey(0xA1, 0x136);
        public static RawKey LeftControl => new RawKey(0xA2, 0x01D);
        public static RawKey RightControl => new RawKey(0xA3, 0x11D);
        public static RawKey LeftAlt => new RawKey(0xA4, 0x038);
        public static RawKey RightAlt => new RawKey(0xA5, 0x138);
        public static RawKey BrowserBack => new RawKey(0xA6, 0x16A);
        public static RawKey BrowserForward => new RawKey(0xA7, 0x169);
        public static RawKey BrowserRefresh => new RawKey(0xA8, 0x167);
        public static RawKey BrowserStop => new RawKey(0xA9, 0x168);
        public static RawKey BrowserSearch => new RawKey(0xAA, 0x165);
        public static RawKey BrowserFavorites => new RawKey(0xAB, 0x166);
        public static RawKey BrowserHome => new RawKey(0xAC, 0x132);
        public static RawKey VolumeMute => new RawKey(0xAD, 0x120);
        public static RawKey VolumeDown => new RawKey(0xAE, 0x12E);
        public static RawKey VolumeUp => new RawKey(0xAF, 0x130);
        public static RawKey MediaNext => new RawKey(0xB0, 0x119);
        public static RawKey MediaPrev => new RawKey(0xB1, 0x110);
        public static RawKey MediaStop => new RawKey(0xB2, 0x124);
        public static RawKey MediaPlayPause => new RawKey(0xB3, 0x122);
        public static RawKey LaunchMail => new RawKey(0xB4, 0x16C);
        public static RawKey LaunchMedia => new RawKey(0xB5, 0x16D);
        public static RawKey LaunchApp1 => new RawKey(0xB6, 0x16B);
        public static RawKey LaunchApp2 => new RawKey(0xB7, 0x121);
        public static RawKey OEM1 => new RawKey(0xBA, 0x027);
        public static RawKey OEMPlus => new RawKey(0xBB, 0x00D);
        public static RawKey OEMComma => new RawKey(0xBC, 0x033);
        public static RawKey OEMMinus => new RawKey(0xBD, 0x00C);
        public static RawKey OEMPeriod => new RawKey(0xBE, 0x034);
        public static RawKey OEM2 => new RawKey(0xBF, 0x035);
        public static RawKey OEM3 => new RawKey(0xC0, 0x029);
        public static RawKey International1 => new RawKey(0xC1, 0x073);
        public static RawKey BrazilianComma => new RawKey(0xC2, 0x07E);
        public static RawKey OEM4 => new RawKey(0xDB, 0x01A);
        public static RawKey OEM5 => new RawKey(0xDC, 0x02B);
        public static RawKey OEM6 => new RawKey(0xDD, 0x01B);
        public static RawKey OEM7 => new RawKey(0xDE, 0x028);
        public static RawKey OEM8 => new RawKey(0xDF, 0x000);
        public static RawKey OEMMax => new RawKey(0xE1, 0x000);
        public static RawKey OEM102 => new RawKey(0xE2, 0x056);
        public static RawKey IcoHelp => new RawKey(0xE3, 0x000);
        public static RawKey Ico00 => new RawKey(0xE4, 0x000);
        public static RawKey ProcessKey => new RawKey(0xE5, 0x000);
        public static RawKey IcoClear => new RawKey(0xE6, 0x000);
        public static RawKey Packet => new RawKey(0xE7, 0x000);
        public static RawKey OEMReset => new RawKey(0xE9, 0x071);
        public static RawKey OEMJump => new RawKey(0xEA, 0x05C);
        public static RawKey International5 => new RawKey(0xEB, 0x07B);
        public static RawKey OEMPa2 => new RawKey(0xEC, 0x000);
        public static RawKey OEMPa3 => new RawKey(0xED, 0x06F);
        public static RawKey OEMWsCtrl => new RawKey(0xEE, 0x05A);
        public static RawKey OEMCUSel => new RawKey(0xEF, 0x000);
        public static RawKey OEMAttn => new RawKey(0xF0, 0x000);
        public static RawKey OEMFinish => new RawKey(0xF1, 0x05B);
        public static RawKey OEMCopy => new RawKey(0xF2, 0x000);
        public static RawKey OEMAuto => new RawKey(0xF3, 0x05F);
        public static RawKey OEMENLW => new RawKey(0xF4, 0x000);
        public static RawKey OEMBackTab => new RawKey(0xF5, 0x05E);
        public static RawKey Attn => new RawKey(0xF6, 0x000);
        public static RawKey CRSel => new RawKey(0xF7, 0x000);
        public static RawKey EXSel => new RawKey(0xF8, 0x000);
        public static RawKey EREOF => new RawKey(0xF9, 0x05D);
        public static RawKey Play => new RawKey(0xFA, 0x000);
        public static RawKey Zoom => new RawKey(0xFB, 0x062);
        public static RawKey Noname => new RawKey(0xFC, 0x000);
        public static RawKey PA1 => new RawKey(0xFD, 0x000);
        public static RawKey OEMClear => new RawKey(0xFE, 0x000);
        public static RawKey International2 => new RawKey(0x00, 0x070);
        public static RawKey International4 => new RawKey(0x00, 0x079);
        public static RawKey International3 => new RawKey(0x00, 0x07D);

        private static Dictionary<RawKey, string> allKeys;
        public static Dictionary<RawKey, string> AllKeys
        {
            get
            {
                if (allKeys == null)
                {
                    allKeys = new Dictionary<RawKey, string>();
                    allKeys.Add(LeftButton, "LeftButton");
                    allKeys.Add(RightButton, "RightButton");
                    allKeys.Add(CtrlBreak, "CtrlBreak");
                    allKeys.Add(MiddleButton, "MiddleButton");
                    allKeys.Add(XButton1, "XButton1");
                    allKeys.Add(XButton2, "XButton2");
                    allKeys.Add(Backspace, "Backspace");
                    allKeys.Add(Tab, "Tab");
                    allKeys.Add(NumpadClear, "NumpadClear");
                    allKeys.Add(Enter, "Enter");
                    allKeys.Add(Shift, "Shift");
                    allKeys.Add(Control, "Control");
                    allKeys.Add(Alt, "Alt");
                    allKeys.Add(Pause, "Pause");
                    allKeys.Add(CapsLock, "CapsLock");
                    allKeys.Add(Kana, "Kana");
                    allKeys.Add(Junja, "Junja");
                    allKeys.Add(Final, "Final");
                    allKeys.Add(Hanja, "Hanja");
                    allKeys.Add(Escape, "Escape");
                    allKeys.Add(Convert, "Convert");
                    allKeys.Add(NonConvert, "NonConvert");
                    allKeys.Add(Accept, "Accept");
                    allKeys.Add(ModeChange, "ModeChange");
                    allKeys.Add(Space, "Space");
                    allKeys.Add(PageUp, "PageUp");
                    allKeys.Add(PageDown, "PageDown");
                    allKeys.Add(End, "End");
                    allKeys.Add(Home, "Home");
                    allKeys.Add(Left, "Left");
                    allKeys.Add(Up, "Up");
                    allKeys.Add(Right, "Right");
                    allKeys.Add(Down, "Down");
                    allKeys.Add(Select, "Select");
                    allKeys.Add(Print, "Print");
                    allKeys.Add(Execute, "Execute");
                    allKeys.Add(PrintScreen, "PrintScreen");
                    allKeys.Add(Insert, "Insert");
                    allKeys.Add(Delete, "Delete");
                    allKeys.Add(Help, "Help");
                    allKeys.Add(N0, "N0");
                    allKeys.Add(N1, "N1");
                    allKeys.Add(N2, "N2");
                    allKeys.Add(N3, "N3");
                    allKeys.Add(N4, "N4");
                    allKeys.Add(N5, "N5");
                    allKeys.Add(N6, "N6");
                    allKeys.Add(N7, "N7");
                    allKeys.Add(N8, "N8");
                    allKeys.Add(N9, "N9");
                    allKeys.Add(A, "A");
                    allKeys.Add(B, "B");
                    allKeys.Add(C, "C");
                    allKeys.Add(D, "D");
                    allKeys.Add(E, "E");
                    allKeys.Add(F, "F");
                    allKeys.Add(G, "G");
                    allKeys.Add(H, "H");
                    allKeys.Add(I, "I");
                    allKeys.Add(J, "J");
                    allKeys.Add(K, "K");
                    allKeys.Add(L, "L");
                    allKeys.Add(M, "M");
                    allKeys.Add(N, "N");
                    allKeys.Add(O, "O");
                    allKeys.Add(P, "P");
                    allKeys.Add(Q, "Q");
                    allKeys.Add(R, "R");
                    allKeys.Add(S, "S");
                    allKeys.Add(T, "T");
                    allKeys.Add(U, "U");
                    allKeys.Add(V, "V");
                    allKeys.Add(W, "W");
                    allKeys.Add(X, "X");
                    allKeys.Add(Y, "Y");
                    allKeys.Add(Z, "Z");
                    allKeys.Add(LeftWindows, "LeftWindows");
                    allKeys.Add(RightWindows, "RightWindows");
                    allKeys.Add(AppsKey, "AppsKey");
                    allKeys.Add(Sleep, "Sleep");
                    allKeys.Add(Numpad0, "Numpad0");
                    allKeys.Add(Numpad1, "Numpad1");
                    allKeys.Add(Numpad2, "Numpad2");
                    allKeys.Add(Numpad3, "Numpad3");
                    allKeys.Add(Numpad4, "Numpad4");
                    allKeys.Add(Numpad5, "Numpad5");
                    allKeys.Add(Numpad6, "Numpad6");
                    allKeys.Add(Numpad7, "Numpad7");
                    allKeys.Add(Numpad8, "Numpad8");
                    allKeys.Add(Numpad9, "Numpad9");
                    allKeys.Add(NumpadMultiply, "NumpadMultiply");
                    allKeys.Add(NumpadAdd, "NumpadAdd");
                    allKeys.Add(Separator, "Separator");
                    allKeys.Add(NumpadSubtract, "NumpadSubtract");
                    allKeys.Add(NumpadDot, "NumpadDot");
                    allKeys.Add(NumpadDivide, "NumpadDivide");
                    allKeys.Add(F1, "F1");
                    allKeys.Add(F2, "F2");
                    allKeys.Add(F3, "F3");
                    allKeys.Add(F4, "F4");
                    allKeys.Add(F5, "F5");
                    allKeys.Add(F6, "F6");
                    allKeys.Add(F7, "F7");
                    allKeys.Add(F8, "F8");
                    allKeys.Add(F9, "F9");
                    allKeys.Add(F10, "F10");
                    allKeys.Add(F11, "F11");
                    allKeys.Add(F12, "F12");
                    allKeys.Add(F13, "F13");
                    allKeys.Add(F14, "F14");
                    allKeys.Add(F15, "F15");
                    allKeys.Add(F16, "F16");
                    allKeys.Add(F17, "F17");
                    allKeys.Add(F18, "F18");
                    allKeys.Add(F19, "F19");
                    allKeys.Add(F20, "F20");
                    allKeys.Add(F21, "F21");
                    allKeys.Add(F22, "F22");
                    allKeys.Add(F23, "F23");
                    allKeys.Add(F24, "F24");
                    allKeys.Add(Numlock, "Numlock");
                    allKeys.Add(ScrollLock, "ScrollLock");
                    allKeys.Add(Jisho, "Jisho");
                    allKeys.Add(Masshou, "Masshou");
                    allKeys.Add(Touroku, "Touroku");
                    allKeys.Add(Loya, "Loya");
                    allKeys.Add(Roya, "Roya");
                    allKeys.Add(LeftButtonAlt, "LeftButtonAlt");
                    allKeys.Add(RightButtonAlt, "RightButtonAlt");
                    allKeys.Add(WheelLeft, "WheelLeft");
                    allKeys.Add(WheelRight, "WheelRight");
                    allKeys.Add(WheelDown, "WheelDown");
                    allKeys.Add(WheelUp, "WheelUp");
                    allKeys.Add(LeftShift, "LeftShift");
                    allKeys.Add(RightShift, "RightShift");
                    allKeys.Add(LeftControl, "LeftControl");
                    allKeys.Add(RightControl, "RightControl");
                    allKeys.Add(LeftAlt, "LeftAlt");
                    allKeys.Add(RightAlt, "RightAlt");
                    allKeys.Add(BrowserBack, "BrowserBack");
                    allKeys.Add(BrowserForward, "BrowserForward");
                    allKeys.Add(BrowserRefresh, "BrowserRefresh");
                    allKeys.Add(BrowserStop, "BrowserStop");
                    allKeys.Add(BrowserSearch, "BrowserSearch");
                    allKeys.Add(BrowserFavorites, "BrowserFavorites");
                    allKeys.Add(BrowserHome, "BrowserHome");
                    allKeys.Add(VolumeMute, "VolumeMute");
                    allKeys.Add(VolumeDown, "VolumeDown");
                    allKeys.Add(VolumeUp, "VolumeUp");
                    allKeys.Add(MediaNext, "MediaNext");
                    allKeys.Add(MediaPrev, "MediaPrev");
                    allKeys.Add(MediaStop, "MediaStop");
                    allKeys.Add(MediaPlayPause, "MediaPlayPause");
                    allKeys.Add(LaunchMail, "LaunchMail");
                    allKeys.Add(LaunchMedia, "LaunchMedia");
                    allKeys.Add(LaunchApp1, "LaunchApp1");
                    allKeys.Add(LaunchApp2, "LaunchApp2");
                    allKeys.Add(OEM1, "OEM1");
                    allKeys.Add(OEMPlus, "OEMPlus");
                    allKeys.Add(OEMComma, "OEMComma");
                    allKeys.Add(OEMMinus, "OEMMinus");
                    allKeys.Add(OEMPeriod, "OEMPeriod");
                    allKeys.Add(OEM2, "OEM2");
                    allKeys.Add(OEM3, "OEM3");
                    allKeys.Add(International1, "International1");
                    allKeys.Add(BrazilianComma, "BrazilianComma");
                    allKeys.Add(OEM4, "OEM4");
                    allKeys.Add(OEM5, "OEM5");
                    allKeys.Add(OEM6, "OEM6");
                    allKeys.Add(OEM7, "OEM7");
                    allKeys.Add(OEM8, "OEM8");
                    allKeys.Add(OEMMax, "OEMMax");
                    allKeys.Add(OEM102, "OEM102");
                    allKeys.Add(IcoHelp, "IcoHelp");
                    allKeys.Add(Ico00, "Ico00");
                    allKeys.Add(ProcessKey, "ProcessKey");
                    allKeys.Add(IcoClear, "IcoClear");
                    allKeys.Add(Packet, "Packet");
                    allKeys.Add(OEMReset, "OEMReset");
                    allKeys.Add(OEMJump, "OEMJump");
                    allKeys.Add(International5, "International5");
                    allKeys.Add(OEMPa2, "OEMPa2");
                    allKeys.Add(OEMPa3, "OEMPa3");
                    allKeys.Add(OEMWsCtrl, "OEMWsCtrl");
                    allKeys.Add(OEMCUSel, "OEMCUSel");
                    allKeys.Add(OEMAttn, "OEMAttn");
                    allKeys.Add(OEMFinish, "OEMFinish");
                    allKeys.Add(OEMCopy, "OEMCopy");
                    allKeys.Add(OEMAuto, "OEMAuto");
                    allKeys.Add(OEMENLW, "OEMENLW");
                    allKeys.Add(OEMBackTab, "OEMBackTab");
                    allKeys.Add(Attn, "Attn");
                    allKeys.Add(CRSel, "CRSel");
                    allKeys.Add(EXSel, "EXSel");
                    allKeys.Add(EREOF, "EREOF");
                    allKeys.Add(Play, "Play");
                    allKeys.Add(Zoom, "Zoom");
                    allKeys.Add(Noname, "Noname");
                    allKeys.Add(PA1, "PA1");
                    allKeys.Add(OEMClear, "OEMClear");
                    allKeys.Add(International2, "International2");
                    allKeys.Add(International4, "International4");
                    allKeys.Add(International3, "International3");
                }
                return allKeys;
            }
        }
    }
}
