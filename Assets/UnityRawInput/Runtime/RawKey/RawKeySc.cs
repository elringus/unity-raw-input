using System.Runtime.CompilerServices;

namespace UnityRawInput
{
    public partial struct RawKey
    {
        /// <summary>
        /// Create a RawKey from a ushort (Scan Code)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RawKey FromScanSode (ushort value)
        {
            switch (value)
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
                case 0x00E: return Back;
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
                case 0x01C: return Return;
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
                case 0x037: return Multiply;
                case 0x038: return LeftMenu;
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
                case 0x04A: return Subtract;
                case 0x04B: return Numpad4;
                case 0x04C: return Numpad5;
                case 0x04D: return Numpad6;
                case 0x04E: return Add;
                case 0x04F: return Numpad1;
                case 0x050: return Numpad2;
                case 0x051: return Numpad3;
                case 0x052: return Numpad0;
                case 0x053: return Decimal;
                case 0x056: return OEM102;
                case 0x057: return F11;
                case 0x058: return F12;
                case 0x05A: return OEMWSCtrl;
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
                case 0x06F: return OEMPA3;
                case 0x071: return OEMReset;
                case 0x073: return International1;
                case 0x076: return F24;
                case 0x07B: return OEMPA1;
                case 0x07E: return BrazilianComma;
                case 0x110: return MediaPrevTrack;
                case 0x119: return MediaNextTrack;
                case 0x11D: return RightControl;
                case 0x120: return VolumeMute;
                case 0x121: return LaunchApplication2;
                case 0x122: return MediaPlayPause;
                case 0x124: return MediaStop;
                case 0x12E: return VolumeDown;
                case 0x130: return VolumeUp;
                case 0x132: return BrowserHome;
                case 0x135: return Divide;
                case 0x136: return RightShift;
                case 0x138: return RightMenu;
                case 0x145: return NumLock;
                case 0x146: return Cancel;
                case 0x15B: return LeftWindows;
                case 0x15C: return RightWindows;
                case 0x15D: return Application;
                case 0x165: return BrowserSearch;
                case 0x166: return BrowserFavorites;
                case 0x167: return BrowserRefresh;
                case 0x168: return BrowserStop;
                case 0x169: return BrowserForward;
                case 0x16A: return BrowserBack;
                case 0x16B: return LaunchApplication1;
                case 0x16C: return LaunchMail;
                case 0x16D: return LaunchMediaSelect;
                default: return new RawKey(0, value);
            }
        }
    }
}
