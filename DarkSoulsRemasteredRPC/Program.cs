using DarkSoulsRemasteredRPC.Manager;
using DiscordRPC;

namespace DarkSoulsRichPresence
{
    internal class Program
    {
        private static readonly DiscordRpcClient client = new DiscordRpcClient("1369323019628974130");
        private static readonly DarkSoulsManager game = new DarkSoulsManager();

        private static void Main(string[] args)
        {
            InitRPC();
            UpdateRPC();

            for (; ; ) { }
        }

        public static void InitRPC()
        {
            client.Initialize();
        }

        public static void UpdateRPC()
        {
            var presence = new RichPresence()
            {
                State = $"Exploring {game.GetAreaName()}",
                Details = "Example Details",
                Assets = new Assets()
                {
                    LargeImageKey = "Example",
                    LargeImageText = "Example Image Text"
                },
                Buttons = new Button[]
                {
                    new Button()
                    {
                        Label = "Example Button",
                        Url = "https://www.google.com/"
                    }
                }
            };

            client.SetPresence(presence);
        }

    }
}