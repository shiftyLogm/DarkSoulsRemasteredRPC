using Process.NET;
using Process.NET.Memory;
using SystemProcess = System.Diagnostics.Process;
using DarkSoulsRemasteredRPC.Services;

namespace DarkSoulsRemasteredRPC.Manager
{
    internal class DarkSoulsManager
    {
        private readonly SystemProcess? gameProcess = SystemProcess.GetProcessesByName("DarkSoulsRemastered").FirstOrDefault();
        private readonly ProcessSharp process;

        public DarkSoulsManager()
        {
            process = new ProcessSharp(gameProcess, MemoryType.Remote);
        }

        private int GetCurrentAreaId()
        {
            IntPtr address = new IntPtr(0x117E4444);
            return process.Memory.Read<int>(address);
        }

        public string GetAreaName() => Area.GetAreaNameById(GetCurrentAreaId());

    }
}
