using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DarkSoulsRemasteredRPC.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Covenant
    {
        [EnumMember(Value = "Null")]
        Nenhum = 101122560,

        [EnumMember(Value = "Way of White")]
        WayOfWhite = 101122561,

        [EnumMember(Value = "Princess Guard")]
        PrincessGuard = 101122562,

        [EnumMember(Value = "Warrior of Sunlight")]
        WarriorOfSunlight = 101122563,

        [EnumMember(Value = "Darkwraith")]
        Darkwraith = 101122564,

        [EnumMember(Value = "Path of the Dragon")]
        PathOfTheDragon = 101122565,

        [EnumMember(Value = "Gravelord Servant")]
        GravelordServant = 101122566,

        [EnumMember(Value = "Forest Hunter")]
        ForestHunter = 101122567,

        [EnumMember(Value = "Blade of the Dark Moon")]
        BladeOfTheDarkMoon = 101122568,

        [EnumMember(Value = "Chaos Servant")]
        ChaosServant = 101122569,
    }
}