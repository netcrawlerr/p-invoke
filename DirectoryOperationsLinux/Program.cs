using System.Runtime.InteropServices;

internal class Program
{
    // read write execute permission
    const int DEFAULT_PERMISSION = 511;

    // signatures

    // for creating dir
    [DllImport("libc", EntryPoint = "mkdir", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int mkdir(string pathname, int mode);

    // for removing dir
    [DllImport("libc", EntryPoint = "rmdir", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int rmdir(string pathname);

    // for renaming dir
    [DllImport("libc", EntryPoint = "rename", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int rename(string oldpath, string newpath);

    static void Main(string[] args)
    {
        // pointin to Desktop
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        Console.WriteLine("<< Linux P/Invoke: Directory Lab >>");
        Console.WriteLine($"Working Directory: {desktopPath}");

        while (true)
        {
            Console.WriteLine("\n Choose an Operation:");
            Console.WriteLine("1. Create Directory");
            Console.WriteLine("2. Rename Directory");
            Console.WriteLine("3. Delete Directory");
            Console.WriteLine("4. Exit");
            Console.WriteLine("\nSelection > ");

            string choice = Console.ReadLine();
            // exit if user chose 4
            if (choice == "4") break;

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter new folder name: ");
                    string newFolder = Path.Combine(desktopPath, Console.ReadLine());

                    // give rrx permission upon creating it
                    if (mkdir(newFolder, DEFAULT_PERMISSION) == 0)
                        Console.WriteLine("Success: Directory Created via P/Invoke");
                    else
                        ShowLinuxError("mkdir");
                    break;

                case "2":
                    Console.WriteLine("Enter EXISTING folder name: ");
                    string oldPath = Path.Combine(desktopPath, Console.ReadLine());

                    Console.WriteLine("Enter NEW folder name: ");
                    string newPath = Path.Combine(desktopPath, Console.ReadLine());

                    if (rename(oldPath, newPath) == 0)
                        Console.WriteLine("Success: Directory Renamed via P/Invoke");
                    else
                        ShowLinuxError("rename");
                    break;

                case "3":
                    Console.WriteLine("Enter folder name to DELETE: ");
                    string deletePath = Path.Combine(desktopPath, Console.ReadLine());

                    if (rmdir(deletePath) == 0)
                        Console.WriteLine("Success: Directory Removed via P/Invoke");
                    else
                        ShowLinuxError("rmdir");
                    break;

                default:
                    Console.WriteLine("Invalid selection");
                    break;

            }
        }
    }

    // error swtich
    static void ShowLinuxError(string functionName)
    {
        int error = Marshal.GetLastPInvokeError();

        Console.WriteLine($"\n[LINUX ERRORNO] {functionName} failed. Code: {error}");

        string message = error switch
        {
            2 => "No such directory",
            13 => "Permission Denied",
            17 => "Directory Already Exists",
            39 => "Directory Not Empty",
            _ => "Unknown Error",
        };

        Console.WriteLine($"Meaning: {message}");
    }
}
