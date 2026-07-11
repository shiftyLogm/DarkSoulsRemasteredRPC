using SystemProcess = System.Diagnostics.Process;

namespace DarkSoulsRemasteredRPC.Utils;

public static class PointerResolver
{
    public static IntPtr ResolveRipRelative(SystemProcess process, string aobPattern)
    {
        IntPtr instruction = AobScanner.FindPattern(process, aobPattern);

        if (instruction == IntPtr.Zero)
            throw new Exception($"Pattern not found: {aobPattern}");

        int relativeOffset = ReadInt(process, instruction + 3);
        IntPtr staticLocation = instruction + relativeOffset + 7;

        return ReadIntPtr(process, staticLocation);
    }

    private static int ReadInt(SystemProcess process, IntPtr address)
    {
        byte[] buffer = new byte[4];
        NativeMethods.ReadProcessMemory(process.Handle, address, buffer, 4, out _);
        return BitConverter.ToInt32(buffer, 0);
    }

    private static IntPtr ReadIntPtr(SystemProcess process, IntPtr address)
    {
        byte[] buffer = new byte[8];
        NativeMethods.ReadProcessMemory(process.Handle, address, buffer, 8, out _);
        return (IntPtr)BitConverter.ToInt64(buffer, 0);
    }
}
