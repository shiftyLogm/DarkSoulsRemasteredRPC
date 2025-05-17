using Process.NET;
using Process.NET.Memory;
using SystemProcess = System.Diagnostics.Process;
using DarkSoulsRemasteredRPC.Utils;
using DarkSoulsRemasteredRPC.Enums;
using DarkSoulsRemasteredRPC.Dictionaries;

namespace DarkSoulsRemasteredRPC.Managers
{
    public class DarkSoulsManager
    {
        private readonly SystemProcess? _gameProcess = SystemProcess.GetProcessesByName("DarkSoulsRemastered").FirstOrDefault();
        private readonly ProcessSharp? _process;

        public DarkSoulsManager()
        {
            try
            {
                _process = new ProcessSharp(_gameProcess, MemoryType.Remote);
            }
            catch (NullReferenceException) { }
        }

        private int ReadProcess(nint addressTarget)
        {
            nint address = new nint(addressTarget);
            return _process!.Memory.Read<int>(address);
        }

        private int GetCurrentAreaId() => ReadProcess(0x1210C444); // Reading actual area value in memory

        public string GetAreaName() => EnumUtilities.GetEnumMemberValueById<Area>(GetCurrentAreaId());

        public int GetCurrentSouls() => ReadProcess(0x120D1EDC); // Reading actual souls value in memory

        private int GetCurrentCovenantId() => ReadProcess(0x120E9723); // Reading actual covenant in memory

        public string GetCurrentCovenantName() => EnumUtilities.GetEnumMemberValueById<Covenant>(GetCurrentCovenantId());

        // Reading covenant string indexed in Discord Application
        // This dictionary returns the name of the image to be displayed
        public string GetCovenantImage()
        {
            Covenant covenant = EnumUtilities.GetEnumValueById<Covenant>(GetCurrentCovenantId());
            return CovenantImage.Covenants[covenant];
        }
    }
}
