using System.Runtime.InteropServices;

internal class Program
{

    // >> some constants here
    const uint MB_OK = 0x00000000;
    const uint MB_ICONINFORMATION = 0x00000040;
    const uint MB_ICONWARNING = 0x00000030;

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int MessageBox(
        IntPtr hWnd,
        string lpText,
        string lpCaption,
        uint uType);

    [DllImport("user32.dll", EntryPoint = "MessageBoxTimeoutW", CharSet = CharSet.Unicode)]
    private static extern int MessageBoxTimeout(
            IntPtr hWnd,
            string lpText,
            string lpCaption,
            uint uType,
            ushort wLanguageId,
            uint dwMilliseconds
        );


    private static void Main(string[] args)
    {
        Console.WriteLine("--- Starting p/invoke ---");

        Console.WriteLine(" << Standard MessageBox >>");
        MessageBox(IntPtr.Zero, "Standard WIN32 MessageBox", "Message Box", 0);

        Console.WriteLine("\nSelf Closing MessageBox in 5 seconds .... ");
        MessageBoxTimeout(
            IntPtr.Zero,
            "This MessageBox will close in 5 seconds",
            "Self Closing MB",
            MB_OK | MB_ICONINFORMATION,
            0,
            5000 // in milliseconds
            );
    }
}