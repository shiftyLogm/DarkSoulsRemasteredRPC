namespace DarkSoulsRemasteredRPC.Models;

public readonly record struct GamePresence(
    string AreaName,
    string Covenant,
    string CovenantImage,
    int SoulsQuantity);