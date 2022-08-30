namespace UnityRawInput
{
    public enum RawMouseState : ushort
    {
        MouseMove = 0x0200,
        LeftButtonDown = 0x0201,
        LeftButtonUp = 0x0202,
        MouseWheel = 0x020A,
        MiddleButtonDown = 0x0207,
        MiddleButtonUp = 0x0208,
        RightButtonDown = 0x0204,
        RightButtonUp = 0x0205,
        ExtraButtonDown = 0x20B,
        ExtraButtonUp = 0x20C
    }

    // https://www.pinvoke.net/default.aspx/Constants/WM.html
}
