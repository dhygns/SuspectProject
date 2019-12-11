using System;
using static SuspectProject.Data.Game;

namespace SuspectProject.Data
{
    [Serializable]
    public class GameUserData : GameDataBase
    {
        // Player's info datas
        public DataPrimitive<string> displayName { get; private set; }
        public DataPrimitive<string> avatarID { get; private set; }
        public DataPrimitive<string> roleID { get; private set; }
        
        // Player's item datas
        public DataList<string> equiedItemIDs { get; private set; }
        public DataList<string> ownedItemIDs { get; private set; }

        // Player's state datas
        public DataPrimitive<float> currentHealth { get; private set; }
        public DataPrimitive<float> currentThrist { get; private set; }
        public DataPrimitive<float> currentHungry { get; private set; }
        public DataPrimitive<float> currentTemperature { get; private set; }
    }
}