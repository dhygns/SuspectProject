using System;
using static SuspectProject.Data.Game;

namespace SuspectProject.Data
{
    [Serializable]
    public class GameItemData : GameDataBase
    {
        public DataPrimitive<string> itemModelID { get; private set; }
        public DataPrimitive<string> ownerID { get; private set; }
    }
}
