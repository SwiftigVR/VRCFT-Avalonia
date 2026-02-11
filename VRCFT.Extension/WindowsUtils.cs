using System.Diagnostics;
using System.Runtime.InteropServices;

namespace VRCFT.Extension;

public static class WindowsUtils
{
    [DllImport("shell32.dll", SetLastError = true)]
    private static extern int SHOpenFolderAndSelectItems(IntPtr pidlFolder, uint cidl, [In, MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl, uint dwFlags);

    [DllImport("shell32.dll", SetLastError = true)]
    private static extern uint SHParseDisplayName([MarshalAs(UnmanagedType.LPWStr)] string name, IntPtr bindingContext, [Out] out IntPtr pidl, uint sfgaoIn, [Out] out uint psfgaoOut);

    public static void OpenFolderAndSelectItem(string filePath)
    {
        if (!OperatingSystem.IsWindows())
        {
            Console.WriteLine("OpenFolderAndSelectItem is only supported on Windows!");
            return;
        }

        filePath = Path.GetFullPath(filePath); // Resolve absolute path
        string folderPath = Path.GetDirectoryName(filePath)!;
        string file = Path.GetFileName(filePath);

        IntPtr nativeFolder;
        uint psfgaoOut;

        SHParseDisplayName(folderPath, IntPtr.Zero, out nativeFolder, 0, out psfgaoOut);

        if (nativeFolder == IntPtr.Zero)
        {
            Console.WriteLine($"Failed to find directory {filePath}!");
            return;
        }

        IntPtr nativeFile;
        SHParseDisplayName(Path.Combine(folderPath, file), IntPtr.Zero, out nativeFile, 0, out psfgaoOut);

        IntPtr[] fileArray;
        // Open the folder without the file selected if we can't find the file
        if (nativeFile == IntPtr.Zero)
            fileArray = new IntPtr[] { nativeFolder };
        else
            fileArray = new IntPtr[] { nativeFile };

        // #define FAILED(hr) (((HRESULT)(hr)) < 0)
        if (SHOpenFolderAndSelectItems(nativeFolder, (uint)fileArray.Length, fileArray, 0) < 0)
            Process.Start("explorer.exe", $"/select,\"{filePath}\"");

        Marshal.FreeCoTaskMem(nativeFolder);
        if (nativeFile != IntPtr.Zero)
            Marshal.FreeCoTaskMem(nativeFile);
    }
}
