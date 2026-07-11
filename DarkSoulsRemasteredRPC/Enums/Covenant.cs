using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DarkSoulsRemasteredRPC.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Covenant
    {
        [EnumMember(Value = "None")]
        Nenhum = 0,

        [EnumMember(Value = "Way of White")]
        WayOfWhite = 328193,

        [EnumMember(Value = "Princess's Guard")]
        PrincessGuard = 328194,

        [EnumMember(Value = "Warrior of Sunlight")]
        WarriorOfSunlight = 328195,

        [EnumMember(Value = "Darkwraith")]
        Darkwraith = 328196,

        [EnumMember(Value = "Path of the Dragon")]
        PathOfTheDragon = 328197,

        [EnumMember(Value = "Gravelord Servant")]
        GravelordServant = 328198,

        [EnumMember(Value = "Forest Hunter")]
        ForestHunter = 328199,

        [EnumMember(Value = "Darkmoon Blade")]
        DarkmoonBlade = 328200,

        [EnumMember(Value = "Chaos Servant")]
        ChaosServant = 328201,
    }
}