using System.Runtime.InteropServices;

internal class Program
{
    // signature
    [DllImport("libgtk-3.so.0", EntryPoint = "gtk_init", CallingConvention = CallingConvention.Cdecl)]
    private static extern void gtk_init(ref int argc, ref IntPtr argv);

    [DllImport("libgtk-3.so.0", EntryPoint = "gtk_message_dialog_new", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern IntPtr gtk_message_dialog_new(
        IntPtr parent,
        int flags,
        int type,
        int buttons,
        string format,
        string message
    );

    [DllImport("libgtk-3.so.0", EntryPoint = "gtk_dialog_run", CallingConvention = CallingConvention.Cdecl)]
    private static extern int gtk_dialog_run(IntPtr dialog);



    [DllImport("libgtk-3.so.0", EntryPoint = "gtk_widget_destroy", CallingConvention = CallingConvention.Cdecl)]
    private static extern void gtk_widget_destroy(IntPtr widget);

    // enums mapped to int

    const int GTK_DIALOG_MODAL = 1 << 0;
    const int GTK_MESSAGE_INFO = 0;
    const int GTK_BUTTONS_OK = 1;

    static void Main(string[] args)
    {
        Console.WriteLine("-- Starting Linux P/Invoke --");

        int argc = 0;
        IntPtr argv = IntPtr.Zero;
        try
        {
            gtk_init(ref argc, ref argv);
        }
        catch (DllNotFoundException)
        {

            Console.WriteLine("Error: libgtk-3.so.0 wasnt found");
            return;
        }

        // now create window
        Console.WriteLine("Incoming ..... >> Dialog Box");

        IntPtr dialog = gtk_message_dialog_new(
            IntPtr.Zero,
            GTK_DIALOG_MODAL,
            GTK_MESSAGE_INFO,
            GTK_BUTTONS_OK,
            "%s",
            "Hello From Native Dialog via P/Invoke!"
        );

        // show the dialong
        gtk_dialog_run(dialog);

        // destroy
        gtk_widget_destroy(dialog);

        Console.WriteLine("Dialog Closed Successfully");
    }

}
