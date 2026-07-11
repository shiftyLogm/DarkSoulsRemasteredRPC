using DarkSoulsRemasteredRPC.Services;
using DarkSoulsRemasteredRPC.Models;
using DiscordRPC;

namespace DarkSoulsRemasteredRPC
{
    public class Program
    {
        private static readonly DiscordRpcClient _client = new DiscordRpcClient("1372757659811319870");
        private static readonly DiscordService _discordClient = new DiscordService();
        private static readonly DarkSoulsProcessService _game = new DarkSoulsProcessService();

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
                string currentCovenantImage = _game.GetCovenantImage();
                int currentSouls = _game.GetCurrentSouls();
                string currentArea = _game.GetCurrentAreaName();
                string currentCovenant = _game.GetCurrentCovenantName();
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
