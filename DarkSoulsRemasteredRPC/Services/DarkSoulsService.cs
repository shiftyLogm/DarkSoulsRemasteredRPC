using DarkSoulsRemasteredRPC.Models;
using Process.NET;
using Process.NET.Memory;
using SystemProcess = System.Diagnostics.Process;
using DarkSoulsRemasteredRPC.Utils;
using DarkSoulsRemasteredRPC.Services.Enums;

namespace DarkSoulsRemasteredRPC.Services;

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
    
    private int ReadSoulCount()
    {
        IntPtr addr = LocatePointer(_baseB, 0x10, 0x94);
        
        return _process!.Memory.Read<int>(addr);
    }

    public int GetCurrentSoulCount() => ReadSoulCount();

    /// <summary>
    /// Resolves the character's active area id
    /// </summary>
    /// <remarks>
    /// Pointer chain: <c>ModuleBase</c> →
    /// dereference → <c>+0x01C88D98</c>
    /// dereference → <c>+0xF74</c> (area id, stored as d).
    /// <para>
    /// Offsets were found via manual inspection in Cheat Engine. No semantic name is
    /// known for <c>+0x113</c> beyond "covenant ID".
    /// </para>
    /// </remarks>
    /// <returns>The raw covenant ID as read from memory.</returns>
    public int ReadAreaName()
    {
        IntPtr addr = LocatePointer(_moduleBase, 0x01C88D98, 0xF74);
        return _process!.Memory.Read<int>(addr);
    }
    
    public string GetCurrentAreaName() =>  EnumUtilities.GetEnumMemberValueById<Area>(ReadAreaName());

    /// <summary>
    /// Resolves the character's active covenant ID.
    /// </summary>
    /// <remarks>
    /// Pointer chain: <c>BaseB</c> → dereference → <c>+0x10</c> (pointer to the player
    /// status sub-struct) → dereference → <c>+0x113</c> (covenant ID, stored as a single byte).
    /// <para>
    /// Offsets were found via manual inspection in Cheat Engine. No semantic name is
    /// known for <c>+0x113</c> beyond "covenant ID".
    /// </para>
    /// </remarks>
    /// <returns>The raw covenant ID as read from memory.</returns>
    private byte ReadCovenantId()
    {
        IntPtr addr = LocatePointer(_baseB, 0x10, 0x113);
        return _process!.Memory.Read<byte>(addr);
    }
    
    public string GetCurrentCovenantName()
    {
        int covenantId = ReadCovenantId();
        
        return EnumUtilities.GetEnumMemberValueById<Covenant>(covenantId);
    }
    
    public string GetCurrentCovenantImage()
    {
        int covenantId = ReadCovenantId();
        
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
            GetCurrentCovenantImage(),
            GetCurrentSoulCount()
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
