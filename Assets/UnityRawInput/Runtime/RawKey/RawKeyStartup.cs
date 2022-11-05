namespace UnityRawInput
{
    public partial struct RawKey
    {
        // https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
        static RawKey ()
        {
            LeftButton = new RawKey(0x01, 0x000);
            RightButton = new RawKey(0x02, 0x000);
            Cancel = new RawKey(0x03, 0x146);
            MiddleButton = new RawKey(0x04, 0x000);
            ExtraButton1 = new RawKey(0x05, 0x000);
            ExtraButton2 = new RawKey(0x06, 0x000);
            Back = new RawKey(0x08, 0x00E);
            Tab = new RawKey(0x09, 0x00F);
            Clear = new RawKey(0x0C, 0x04C);
            Return = new RawKey(0x0D, 0x01C);
            Shift = new RawKey(0x10, 0x02A);
            Control = new RawKey(0x11, 0x01D);
            Menu = new RawKey(0x12, 0x038);
            Pause = new RawKey(0x13, 0x045);
            CapsLock = new RawKey(0x14, 0x03A);
            Kana = new RawKey(0x15, 0x000);
            Junja = new RawKey(0x17, 0x000);
            Final = new RawKey(0x18, 0x000);
            Hanja = new RawKey(0x19, 0x000);
            Escape = new RawKey(0x1B, 0x001);
            Convert = new RawKey(0x1C, 0x000);
            NonConvert = new RawKey(0x1D, 0x000);
            Accept = new RawKey(0x1E, 0x000);
            ModeChange = new RawKey(0x1F, 0x000);
            Space = new RawKey(0x20, 0x039);
            Prior = new RawKey(0x21, 0x049);
            Next = new RawKey(0x22, 0x051);
            End = new RawKey(0x23, 0x04F);
            Home = new RawKey(0x24, 0x047);
            Left = new RawKey(0x25, 0x04B);
            Up = new RawKey(0x26, 0x048);
            Right = new RawKey(0x27, 0x04D);
            Down = new RawKey(0x28, 0x050);
            Select = new RawKey(0x29, 0x000);
            Print = new RawKey(0x2A, 0x000);
            Execute = new RawKey(0x2B, 0x000);
            Snapshot = new RawKey(0x2C, 0x154);
            Insert = new RawKey(0x2D, 0x052);
            Delete = new RawKey(0x2E, 0x053);
            Help = new RawKey(0x2F, 0x063);
            N0 = new RawKey(0x30, 0x00B);
            N1 = new RawKey(0x31, 0x002);
            N2 = new RawKey(0x32, 0x003);
            N3 = new RawKey(0x33, 0x004);
            N4 = new RawKey(0x34, 0x005);
            N5 = new RawKey(0x35, 0x006);
            N6 = new RawKey(0x36, 0x007);
            N7 = new RawKey(0x37, 0x008);
            N8 = new RawKey(0x38, 0x009);
            N9 = new RawKey(0x39, 0x00A);
            A = new RawKey(0x41, 0x01E);
            B = new RawKey(0x42, 0x030);
            C = new RawKey(0x43, 0x02E);
            D = new RawKey(0x44, 0x020);
            E = new RawKey(0x45, 0x012);
            F = new RawKey(0x46, 0x021);
            G = new RawKey(0x47, 0x022);
            H = new RawKey(0x48, 0x023);
            I = new RawKey(0x49, 0x017);
            J = new RawKey(0x4A, 0x024);
            K = new RawKey(0x4B, 0x025);
            L = new RawKey(0x4C, 0x026);
            M = new RawKey(0x4D, 0x032);
            N = new RawKey(0x4E, 0x031);
            O = new RawKey(0x4F, 0x018);
            P = new RawKey(0x50, 0x019);
            Q = new RawKey(0x51, 0x010);
            R = new RawKey(0x52, 0x013);
            S = new RawKey(0x53, 0x01F);
            T = new RawKey(0x54, 0x014);
            U = new RawKey(0x55, 0x016);
            V = new RawKey(0x56, 0x02F);
            W = new RawKey(0x57, 0x011);
            X = new RawKey(0x58, 0x02D);
            Y = new RawKey(0x59, 0x015);
            Z = new RawKey(0x5A, 0x02C);
            LeftWindows = new RawKey(0x5B, 0x15B);
            RightWindows = new RawKey(0x5C, 0x15C);
            Application = new RawKey(0x5D, 0x15D);
            Sleep = new RawKey(0x5F, 0x05F);
            Numpad0 = new RawKey(0x60, 0x052);
            Numpad1 = new RawKey(0x61, 0x04F);
            Numpad2 = new RawKey(0x62, 0x050);
            Numpad3 = new RawKey(0x63, 0x051);
            Numpad4 = new RawKey(0x64, 0x04B);
            Numpad5 = new RawKey(0x65, 0x04C);
            Numpad6 = new RawKey(0x66, 0x04D);
            Numpad7 = new RawKey(0x67, 0x047);
            Numpad8 = new RawKey(0x68, 0x048);
            Numpad9 = new RawKey(0x69, 0x049);
            Multiply = new RawKey(0x6A, 0x037);
            Add = new RawKey(0x6B, 0x04E);
            Separator = new RawKey(0x6C, 0x000);
            Subtract = new RawKey(0x6D, 0x04A);
            Decimal = new RawKey(0x6E, 0x053);
            Divide = new RawKey(0x6F, 0x135);
            F1 = new RawKey(0x70, 0x03B);
            F2 = new RawKey(0x71, 0x03C);
            F3 = new RawKey(0x72, 0x03D);
            F4 = new RawKey(0x73, 0x03E);
            F5 = new RawKey(0x74, 0x03F);
            F6 = new RawKey(0x75, 0x040);
            F7 = new RawKey(0x76, 0x041);
            F8 = new RawKey(0x77, 0x042);
            F9 = new RawKey(0x78, 0x043);
            F10 = new RawKey(0x79, 0x044);
            F11 = new RawKey(0x7A, 0x057);
            F12 = new RawKey(0x7B, 0x058);
            F13 = new RawKey(0x7C, 0x064);
            F14 = new RawKey(0x7D, 0x065);
            F15 = new RawKey(0x7E, 0x066);
            F16 = new RawKey(0x7F, 0x067);
            F17 = new RawKey(0x80, 0x068);
            F18 = new RawKey(0x81, 0x069);
            F19 = new RawKey(0x82, 0x06A);
            F20 = new RawKey(0x83, 0x06B);
            F21 = new RawKey(0x84, 0x06B);
            F22 = new RawKey(0x85, 0x06D);
            F23 = new RawKey(0x86, 0x06E);
            F24 = new RawKey(0x87, 0x076);
            NumLock = new RawKey(0x90, 0x145);
            ScrollLock = new RawKey(0x91, 0x046);
            Fujitsu_Jisho = new RawKey(0x92, 0x000);
            Fujitsu_Masshou = new RawKey(0x93, 0x000);
            Fujitsu_Touroku = new RawKey(0x94, 0x000);
            Fujitsu_Loya = new RawKey(0x95, 0x000);
            Fujitsu_Roya = new RawKey(0x96, 0x000);
            LeftButtonAlt = new RawKey(0x9A, 0x000);
            RightButtonAlt = new RawKey(0x9B, 0x000);
            WheelLeft = new RawKey(0x9C, 0x001);
            WheelRight = new RawKey(0x9D, 0x001);
            WheelDown = new RawKey(0x9E, 0x001);
            WheelUp = new RawKey(0x9F, 0x001);
            LeftShift = new RawKey(0xA0, 0x02A);
            RightShift = new RawKey(0xA1, 0x136);
            LeftControl = new RawKey(0xA2, 0x01D);
            RightControl = new RawKey(0xA3, 0x11D);
            LeftMenu = new RawKey(0xA4, 0x038);
            RightMenu = new RawKey(0xA5, 0x138);
            BrowserBack = new RawKey(0xA6, 0x16A);
            BrowserForward = new RawKey(0xA7, 0x169);
            BrowserRefresh = new RawKey(0xA8, 0x167);
            BrowserStop = new RawKey(0xA9, 0x168);
            BrowserSearch = new RawKey(0xAA, 0x165);
            BrowserFavorites = new RawKey(0xAB, 0x166);
            BrowserHome = new RawKey(0xAC, 0x132);
            VolumeMute = new RawKey(0xAD, 0x120);
            VolumeDown = new RawKey(0xAE, 0x12E);
            VolumeUp = new RawKey(0xAF, 0x130);
            MediaNextTrack = new RawKey(0xB0, 0x119);
            MediaPrevTrack = new RawKey(0xB1, 0x110);
            MediaStop = new RawKey(0xB2, 0x124);
            MediaPlayPause = new RawKey(0xB3, 0x122);
            LaunchMail = new RawKey(0xB4, 0x16C);
            LaunchMediaSelect = new RawKey(0xB5, 0x16D);
            LaunchApplication1 = new RawKey(0xB6, 0x16B);
            LaunchApplication2 = new RawKey(0xB7, 0x121);
            OEM1 = new RawKey(0xBA, 0x027);
            OEMPlus = new RawKey(0xBB, 0x00D);
            OEMComma = new RawKey(0xBC, 0x033);
            OEMMinus = new RawKey(0xBD, 0x00C);
            OEMPeriod = new RawKey(0xBE, 0x034);
            OEM2 = new RawKey(0xBF, 0x035);
            OEM3 = new RawKey(0xC0, 0x029);
            International1 = new RawKey(0xC1, 0x073);
            BrazilianComma = new RawKey(0xC2, 0x07E);
            OEM4 = new RawKey(0xDB, 0x01A);
            OEM5 = new RawKey(0xDC, 0x02B);
            OEM6 = new RawKey(0xDD, 0x01B);
            OEM7 = new RawKey(0xDE, 0x028);
            OEM8 = new RawKey(0xDF, 0x000);
            OEMAX = new RawKey(0xE1, 0x000);
            OEM102 = new RawKey(0xE2, 0x056);
            ICOHelp = new RawKey(0xE3, 0x000);
            ICO00 = new RawKey(0xE4, 0x000);
            ProcessKey = new RawKey(0xE5, 0x000);
            ICOClear = new RawKey(0xE6, 0x000);
            Packet = new RawKey(0xE7, 0x000);
            OEMReset = new RawKey(0xE9, 0x071);
            OEMJump = new RawKey(0xEA, 0x05C);
            OEMPA1 = new RawKey(0xEB, 0x07B);
            OEMPA2 = new RawKey(0xEC, 0x000);
            OEMPA3 = new RawKey(0xED, 0x06F);
            OEMWSCtrl = new RawKey(0xEE, 0x05A);
            OEMCUSel = new RawKey(0xEF, 0x000);
            OEMATTN = new RawKey(0xF0, 0x000);
            OEMFinish = new RawKey(0xF1, 0x05B);
            OEMCopy = new RawKey(0xF2, 0x000);
            OEMAuto = new RawKey(0xF3, 0x05F);
            OEMENLW = new RawKey(0xF4, 0x000);
            OEMBackTab = new RawKey(0xF5, 0x05E);
            ATTN = new RawKey(0xF6, 0x000);
            CRSel = new RawKey(0xF7, 0x000);
            EXSel = new RawKey(0xF8, 0x000);
            EREOF = new RawKey(0xF9, 0x05D);
            Play = new RawKey(0xFA, 0x000);
            Zoom = new RawKey(0xFB, 0x062);
            Noname = new RawKey(0xFC, 0x000);
            PA1 = new RawKey(0xFD, 0x000);
            OEMClear = new RawKey(0xFE, 0x000);
            International2 = new RawKey(0x00, 0x070);
            International4 = new RawKey(0x00, 0x079);
            International3 = new RawKey(0x00, 0x07D);
        }

        /// <summary>
        /// Ensures that any environment with UnityRawInput will have
        /// <a cref="UnityEngine.Application.runInBackground"/> enabled
        /// </summary>
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void EnsureBackground ()
        {
            if (UnityEngine.Application.runInBackground) return;
            UnityEngine.Debug.LogWarning("Application isn't set to run in background! Not enabling this option will " +
                "cause severe mouse slowdown if the window isn't in focus. Enabling behavior for this playsession, " +
                "but you should explicitly enable this in \"Build Settings→Player Settings→Player→Resolution and " +
                "Presentation→Run In Background\".");
            UnityEngine.Application.runInBackground = true;
        }
    }
}
