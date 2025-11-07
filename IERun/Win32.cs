using System.Runtime.InteropServices;

namespace IERun;

public class Win32 {
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    public const int SW_MAXIMIZE = 3;
}
