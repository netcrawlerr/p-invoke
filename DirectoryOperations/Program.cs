using System.Runtime.InteropServices;

internal class Program
{
    // signatures
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern bool CreateDirectory(string lpPathName, IntPtr lpSecurityAttributes);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern bool RemoveDirectory(string lpPathName);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern bool MoveFile(string lpExistingFileName, string lpNewFileName);


    static void Main(string[] args) 
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        Console.WriteLine("<< A Little Bit of p-invoke: Directory Lab >>");
        Console.WriteLine($"Working Directory: {desktopPath}");

        while (true)
        {
            Console.WriteLine("\nChoose an operation:");
            Console.WriteLine("1. Create Directory");
            Console.WriteLine("2. Rename Directory");
            Console.WriteLine("3. Delete Directory");
            Console.WriteLine("4. Exit");
            Console.Write("\nSelection > ");

            string choice = Console.ReadLine();

            if (choice == "4") break;

            switch (choice)
            {
                case "1":
                    Console.Write("Enter new folder name: ");
                    string newFolder = Path.Combine(desktopPath, Console.ReadLine());
                    if (CreateDirectory(newFolder, IntPtr.Zero))
                        Console.WriteLine("Success: Directory created via p-invoke.");
                    else
                        ShowError("CreateDirectory");
                    break;
                case "2":
                    Console.Write("Enter EXISTING folder name: ");
                    string oldPath = Path.Combine(desktopPath, Console.ReadLine());
                    Console.Write("Enter NEW folder name: ");
                    string newPath = Path.Combine(desktopPath, Console.ReadLine());

                    if (MoveFile(oldPath, newPath))
                        Console.WriteLine("Success: Directory renamed.");
                    else
                        ShowError("MoveFile");
                    break;

                case "3":
                    Console.Write("Enter folder name to DELETE: ");
                    string deletePath = Path.Combine(desktopPath, Console.ReadLine());
                    if (RemoveDirectory(deletePath))
                        Console.WriteLine("Success: Directory removed.");
                    else
                        ShowError("RemoveDirectory");
                    break;

                default:
                    Console.WriteLine("Invalid selection.");
                    break;
            }

        }

    }

    static void ShowError(string functionName)
    {
        int error = Marshal.GetLastWin32Error();
        Console.WriteLine($"\n[WIN32 ERROR] {functionName} failed. Code: {error}");

        // Win32 Error Codes
        string message = error switch
        {
            2 => "File/Folder not found.",
            3 => "Path not found (Check your spelling).",
            5 => "Access Denied (Try running as Admin).",
            183 => "Folder already exists.",
            _ => "Unknown OS error."
        };
        Console.WriteLine($"Meaning: {message}");
    }


}