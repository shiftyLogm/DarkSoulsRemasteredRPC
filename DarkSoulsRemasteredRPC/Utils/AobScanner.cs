namespace DarkSoulsRemasteredRPC.Utils;

public static class AobScanner
{
    public static IntPtr FindPattern(System.Diagnostics.Process process, string pattern)
    {
        var module = process.MainModule!;
        IntPtr baseAddress = module.BaseAddress;
        int moduleSize = module.ModuleMemorySize;

        byte[] moduleBytes = ReadProcessMemory(process, baseAddress, moduleSize);
        var (patternBytes, mask) = ParsePattern(pattern);

        for (int i = 0; i <= moduleBytes.Length - patternBytes.Length; i++)
        {
            bool found = true;
            for (int j = 0; j < patternBytes.Length; j++)
            {
                if (mask[j] && moduleBytes[i + j] != patternBytes[j])
                {
                    found = false;
                    break;
                }
            }

            if (found)
                return baseAddress + i;
        }

        return IntPtr.Zero;
    }

    private static (byte[] pattern, bool[] mask) ParsePattern(string pattern)
    {
        var parts = pattern.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var patternBytes = new byte[parts.Length];
        var mask = new bool[parts.Length];

        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i] == "??" || parts[i] == "?")
            {
                patternBytes[i] = 0;
                mask[i] = false;
            }
            else
            {
                patternBytes[i] = Convert.ToByte(parts[i], 16);
                mask[i] = true;
            }
        }

        return (patternBytes, mask);
    }

    private static byte[] ReadProcessMemory(System.Diagnostics.Process process, IntPtr baseAddress, int size)
    {
        byte[] buffer = new byte[size];
        IntPtr handle = process.Handle;
        NativeMethods.ReadProcessMemory(handle, baseAddress, buffer, size, out _);
        return buffer;
    }
}

internal static class NativeMethods
{
    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    public static extern bool ReadProcessMemory(
        IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);
}