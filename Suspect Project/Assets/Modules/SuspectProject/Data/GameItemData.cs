using System;
using static SuspectProject.Data.Game;

namespace SuspectProject.Data
{
    [Serializable]
    public class GameItemData : GameDataBase
    {
        public GameDataPrimitive<string> itemModelID { get; private set; }
        public GameDataPrimitive<string> ownerID { get; private set; }
    }
}
