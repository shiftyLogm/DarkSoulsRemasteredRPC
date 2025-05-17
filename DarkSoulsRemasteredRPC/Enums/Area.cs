using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DarkSoulsRemasteredRPC.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Area
    {
        [EnumMember(Value = "Depths")]
        Depths = 1000,

        [EnumMember(Value = "Undead Burg")]
        UndeadBurg = 1010,

        [EnumMember(Value = "Undead Parish")]
        UndeadParish = 1011,

        [EnumMember(Value = "Firelink Shrine")]
        FirelinkShrine = 1020,

        [EnumMember(Value = "Painted World of Ariamis")]
        PaintedWorldOfAriamis = 1100,

        [EnumMember(Value = "Darkroot Garden")]
        DarkrootGarden = 1200,

        [EnumMember(Value = "Darkroot Basin")]
        DarkrootBasin = 1201,

        [EnumMember(Value = "Sanctuary Garden")]
        SanctuaryGarden = 1210,

        [EnumMember(Value = "Oolacile Sanctuary")]
        OolacileSanctuary = 1211,

        [EnumMember(Value = "Royal Wood")]
        RoyalWood = 1212,

        [EnumMember(Value = "Oolacile Township")]
        OolacileTownship = 1213,

        [EnumMember(Value = "Chasm of the Abyss")]
        ChasmOfTheAbyss = 1214,

        [EnumMember(Value = "Battle of Stoicism")]
        BattleOfStoicism = 1215,

        [EnumMember(Value = "Battle of Stoicism Gazebo")]
        BattleOfStoicismGazebo = 1216,

        [EnumMember(Value = "The Catacombs")]
        TheCatacombs = 1300,

        [EnumMember(Value = "Tomb of the Giants")]
        TombOfTheGiants = 1310,

        [EnumMember(Value = "The Great Hollow")]
        TheGreatHollow = 1320,

        [EnumMember(Value = "Ash Lake")]
        AshLake = 1321,

        [EnumMember(Value = "Blighttown")]
        Blighttown = 1400,

        [EnumMember(Value = "Quelaag's Domain")]
        QuelaagsDomain = 1401,

        [EnumMember(Value = "Demon Ruins")]
        DemonRuins = 1410,

        [EnumMember(Value = "Lost Izalith")]
        LostIzalith = 1411,

        [EnumMember(Value = "Sen's Fortress")]
        SensFortress = 1500,

        [EnumMember(Value = "Anor Londo")]
        AnorLondo = 1510,

        [EnumMember(Value = "New Londo Ruins")]
        NewLondoRuins = 1600,

        [EnumMember(Value = "The Abyss")]
        TheAbyss = 1601,

        [EnumMember(Value = "Valley of Drakes")]
        ValleyOfDrakes = 1602,

        [EnumMember(Value = "The Duke's Archives")]
        TheDukesArchives = 1700,

        [EnumMember(Value = "Crystal Cave")]
        CrystalCave = 1701,

        [EnumMember(Value = "Kiln of the First Flame")]
        KilnOfTheFirstFlame = 1800,

        [EnumMember(Value = "Firelink Altar")]
        FirelinkAltar = 1801,

        [EnumMember(Value = "Northern Undead Asylum")]
        NorthernUndeadAsylum = 1810,

        [EnumMember(Value = "Sunlight Altar")]
        SunlightAltar = 2001,

        [EnumMember(Value = "Chamber of the Princess")]
        ChamberOfThePrincess = 2003,

        [EnumMember(Value = "Darkmoon Tomb")]
        DarkmoonTomb = 2004,

        [EnumMember(Value = "Daughter of Chaos")]
        DaughterOfChaos = 2006,

        [EnumMember(Value = "Altar of the Gravelord")]
        AltarOfTheGravelord = 2007,

        [EnumMember(Value = "Stone Dragon")]
        StoneDragon = 2008,

        [EnumMember(Value = "Oolacile Township Dungeon")]
        OolacileTownshipDungeon = 2013
    }
}