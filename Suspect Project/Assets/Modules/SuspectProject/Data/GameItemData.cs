using System;

namespace SuspectProject.Data
{
    [Serializable]
    public class GameItemData : GameDataBase
    {
        public GameDataPrimitive<string> itemModelID { get; private set; }
        public GameDataPrimitive<string> ownerID { get; private set; }
    }
}
