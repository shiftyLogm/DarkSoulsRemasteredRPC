using Process.NET;
using Process.NET.Memory;
using SystemProcess = System.Diagnostics.Process;
using DarkSoulsRemasteredRPC.Utils;
using DarkSoulsRemasteredRPC.Services.Enums;

namespace DarkSoulsRemasteredRPC.Services
{
    public class DarkSoulsProcessService
    {
        private const string ProcessName = "DarkSoulsRemastered";
        
        private readonly SystemProcess? _gameProcess = SystemProcess.GetProcessesByName(ProcessName).FirstOrDefault();
        private readonly ProcessSharp? _process;
        
        public DarkSoulsProcessService()
        {
            try
            {
                _process = new ProcessSharp(_gameProcess, MemoryType.Remote);
            }
            catch (NullReferenceException) { }
        }

        private IntPtr GetBaseB()
        {
            return PointerResolver.ResolveRipRelative(_gameProcess!,
                "48 8B 05 ?? ?? ?? ?? 45 33 ED 48 8B F1 48 85 C0");
        }

        private byte GetCurrentCovenant()
        {
            IntPtr baseB = GetBaseB();

            IntPtr intermediate = _process.Memory.Read<IntPtr>(baseB + 0x10);
            IntPtr finalAddress = intermediate + 0x113;

            return _process.Memory.Read<byte>(finalAddress);
        }

        public int GetCurrentSouls()
        {
            IntPtr baseB = GetBaseB();
            
            IntPtr intermediate = _process.Memory.Read<IntPtr>(baseB + 0x10);
            IntPtr finalAddress = intermediate + 0x94; 
            
            return _process.Memory.Read<int>(finalAddress);
        }
        
        public string GetCurrentAreaName() => "Rio de Janeiro";
        public string GetCurrentCovenantName()
        {
            byte currentCovenant = GetCurrentCovenant();
            
            return EnumUtilities.GetEnumMemberValueById<Covenant>(currentCovenant);
        }

        // Reading covenant string indexed in Discord Application
        public string GetCovenantImage()
        {
            Covenant covenant = (Covenant)EnumUtilities.GetEnumIdByMemberValue<Covenant>(GetCurrentCovenantName());;
            return covenant switch
            {
                Covenant.WayOfWhite => "way-of-white-covenant",
                Covenant.PrincessGuard => "princess-guard-covenant",
                Covenant.WarriorOfSunlight => "warrior-of-sunlight-covenant",
                Covenant.Darkwraith => "darkwraith-covenant",
                Covenant.PathOfTheDragon => "path-of-the-dragon-covenant",
                Covenant.GravelordServant => "gravelord-servant-covenant",
                Covenant.ForestHunter => "forest-hunter-covenant",
                Covenant.DarkmoonBlade => "blade-of-darkmoon-covenant",
                Covenant.ChaosServant => "chaos-servant-covenant",
                _ => "rpc_default_logo",
            };
        }
    }
}
