using DarkSoulsRemasteredRPC.Enums;

namespace DarkSoulsRemasteredRPC.Dictionaries
{
    public static class CovenantImage
    {
        public static readonly Dictionary<Covenant, string> Covenants = new Dictionary<Covenant, string>()
        {
            { Covenant.Nenhum, "rpc_default_logo" },
            { Covenant.WayOfWhite, "way-of-white-covenant" },
            { Covenant.PrincessGuard, "princess-guard-covenant" },
            { Covenant.WarriorOfSunlight, "warrior-of-sunlight-covenant" },
            { Covenant.Darkwraith, "darkwraith-covenant" },
            { Covenant.PathOfTheDragon, "path-of-the-dragon-covenant" },
            { Covenant.GravelordServant, "gravelord-servant-covenant" },
            { Covenant.ForestHunter, "forest-hunter-covenant" },
            { Covenant.BladeOfTheDarkMoon, "blade-of-darkmoon-covenant" },
            { Covenant.ChaosServant, "chaos-servant-covenant" },
        };
    }
}
