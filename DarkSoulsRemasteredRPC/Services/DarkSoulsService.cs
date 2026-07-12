using DarkSoulsRemasteredRPC.Models;
using Process.NET;
using Process.NET.Memory;
using SystemProcess = System.Diagnostics.Process;
using DarkSoulsRemasteredRPC.Utils;
using DarkSoulsRemasteredRPC.Services.Enums;

namespace DarkSoulsRemasteredRPC.Services
{
    public class DarkSoulsService
    {
        private const string ProcessName = "DarkSoulsRemastered";

        private readonly SystemProcess? _gameProcess;
        private readonly ProcessSharp? _process;
        private readonly IntPtr _moduleBase;
        private readonly IntPtr _baseB;
        
        public DarkSoulsService()
        {
            try
            {
                _gameProcess = SystemProcess.GetProcessesByName(ProcessName).FirstOrDefault();
                _process = new ProcessSharp(_gameProcess, MemoryType.Remote);
                
                _moduleBase = _gameProcess!.MainModule!.BaseAddress;
                _baseB = PointerResolver.ResolveRipRelative(_gameProcess!, "48 8B 05 ?? ?? ?? ?? 45 33 ED 48 8B F1 48 85 C0");
            }
            catch (NullReferenceException) { }
        }
        
        public int GetCurrentSouls()
        {
            IntPtr addr = LocatePointer(_baseB, 0x10, 0x94);
            
            return _process!.Memory.Read<int>(addr);
        }

        public string GetCurrentAreaName()
        {
            IntPtr addr = LocatePointer(_moduleBase, 0x01C88D98, 0xF74);
            int areaId = _process!.Memory.Read<int>(addr);
            
            return EnumUtilities.GetEnumMemberValueById<Area>(areaId);
        }
        
        public string GetCurrentCovenantName()
        {
            IntPtr addr = LocatePointer(_baseB, 0x10, 0x113);
            int covenantId = _process!.Memory.Read<byte>(addr);
            
            return EnumUtilities.GetEnumMemberValueById<Covenant>(covenantId);
        }
        
        public string GetCovenantImage()
        {
            IntPtr addr = LocatePointer(_baseB, 0x10, 0x113);
            int covenantId = _process!.Memory.Read<byte>(addr);
            
            return (Covenant)covenantId switch
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
        
        public GamePresence GetCurrentPresence()
        {
            return new GamePresence(
                GetCurrentAreaName(),
                GetCurrentCovenantName(),
                GetCovenantImage(),
                GetCurrentSouls()
            );
        }
        
        #region Helpers
        
        private IntPtr LocatePointer(IntPtr baseAddress, params nint[] offsets)
        {
            IntPtr address = baseAddress;

            for (int i = 0; i < offsets.Length - 1; i++)
            {
                address = _process!.Memory.Read<IntPtr>(address + offsets[i]);
            }

            return address + offsets[^1];
        }
        
        #endregion
    }
}
