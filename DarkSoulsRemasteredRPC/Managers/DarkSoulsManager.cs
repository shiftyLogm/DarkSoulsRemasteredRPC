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
        
        public int GetCurrentSouls() => ReadProcess(0x0C5546A4); 
        public string GetCurrentAreaName() => EnumUtilities.GetEnumMemberValueById<Area>(ReadProcess(0x27F70274));
        public string GetCurrentCovenantName() => EnumUtilities.GetEnumMemberValueById<Covenant>(ReadProcess(0x0C554723));
        
        // Reading covenant string indexed in Discord Application
        // This dictionary returns the name of the image to be displayed
        public string GetCovenantImage()
        {
            Covenant covenant = (Covenant)EnumUtilities.GetEnumIdByMemberValue<Covenant>(GetCurrentCovenantName());;
            return CovenantImage.Covenants[covenant];
        }
    }
}
