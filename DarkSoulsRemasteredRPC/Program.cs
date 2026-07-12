using DarkSoulsRemasteredRPC.Services;
using DarkSoulsRemasteredRPC.Models;
using DiscordRPC;

const string DISCORD_APP_ID = "1372757659811319870";

using DiscordRpcClient client = new DiscordRpcClient(DISCORD_APP_ID);

DarkSoulsService game = new DarkSoulsService();

client.Initialize();

await UpdatePresenceLoop();

async Task UpdatePresenceLoop()
{
    GamePresence? lastPresence = null;

    while (true)
    {
        try
        {
            GamePresence currentPresence = game.GetCurrentPresence();

            if (!currentPresence.Equals(lastPresence))
            {
                UpdateRPC(currentPresence);
                lastPresence = currentPresence;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"RPC Error: {ex.Message}");
        }

        await Task.Delay(2000);
    }
}

void UpdateRPC(GamePresence gamePresence)
{
    RichPresence presence = new RichPresence()
    {
        State = $"{gamePresence.SoulsQuantity} Souls",
        Details = $"Exploring {gamePresence.AreaName}",
        Assets = new Assets()
        {
            LargeImageKey = gamePresence.CovenantImage,
            LargeImageText = gamePresence.Covenant,
        },
        StatusDisplay = StatusDisplayType.Name,
        Type = ActivityType.Playing,
    };

    client.SetPresence(presence);
}