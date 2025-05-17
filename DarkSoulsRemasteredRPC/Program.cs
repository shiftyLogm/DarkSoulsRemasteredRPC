using DarkSoulsRemasteredRPC.Enums;
using DarkSoulsRemasteredRPC.Managers;
using DarkSoulsRemasteredRPC.Structs;
using DarkSoulsRemasteredRPC.Utils;
using DiscordRPC;

namespace DarkSoulsRichPresence
{
    public class Program
    {
        private static readonly DiscordRpcClient _client = new DiscordRpcClient("1372757659811319870");
        private static readonly DiscordManager _discordClient = new DiscordManager();
        private static readonly DarkSoulsManager _game = new DarkSoulsManager();

        private static void Main(string[] args)
        {
            InitRPC();

            Thread updateThread = new Thread(UpdatePresenceLoop);
            updateThread.IsBackground = true;
            updateThread.Start();

            for (; ; ) { }
        }

        private static void InitRPC()
        {
            _client.Initialize();
            _client.Invoke();
        }

        private static void UpdatePresenceLoop()
        {
            while (true)
            {
                string currentArea = _game.GetAreaName();
                string currentCovenant = _game.GetCurrentCovenantName();
                string currentCovenantImage = _game.GetCovenantImage();
                int currentSouls = _game.GetCurrentSouls();
                UpdateRPC(new GamePresence(currentArea, currentCovenant, currentCovenantImage, currentSouls));
                Thread.Sleep(2000);
            }

        }

        private static void UpdateRPC(GamePresence gamePresence)
        {
            var presence = new RichPresence()
            {
                State = $"{gamePresence.SoulsQuantity} Souls",
                Details = $"Exploring {gamePresence.AreaName}",
                Assets = new Assets()
                {
                    LargeImageKey = gamePresence.CovenantImage,
                    LargeImageText = $"This user has entered a covenant with the {gamePresence.Covenant}"
                },
            };

            _client.SetPresence(presence);
        }

    }
}