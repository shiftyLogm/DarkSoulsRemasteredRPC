using DarkSoulsRemasteredRPC.Enums;

namespace DarkSoulsRemasteredRPC.Structs
{
    public struct GamePresence
    {
        public string AreaName;
        public string Covenant;
        public string CovenantImage;
        public int SoulsQuantity;

        public GamePresence
        (
            string _AreaName,
            string _Covenant,
            string _CovenantImage,
            int _SoulsQuantity
        )
        {
            AreaName = _AreaName;
            SoulsQuantity = _SoulsQuantity;
            Covenant = _Covenant;
            CovenantImage = _CovenantImage;
        }
    }
}
