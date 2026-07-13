using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DarkSoulsRemasteredRPC.Services.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Covenant
{
    [EnumMember(Value = "None")]
    None = 0,

    [EnumMember(Value = "Way of White")]
    WayOfWhite = 1,

    [EnumMember(Value = "Princess's Guard")]
    PrincessGuard = 2,

    [EnumMember(Value = "Warrior of Sunlight")]
    WarriorOfSunlight = 3,

    [EnumMember(Value = "Darkwraith")]
    Darkwraith = 4,

    [EnumMember(Value = "Path of the Dragon")]
    PathOfTheDragon = 5,

    [EnumMember(Value = "Gravelord Servant")]
    GravelordServant = 6,

    [EnumMember(Value = "Forest Hunter")]
    ForestHunter = 7,

    [EnumMember(Value = "Darkmoon Blade")]
    DarkmoonBlade = 8,

    [EnumMember(Value = "Chaos Servant")]
    ChaosServant = 9,
}