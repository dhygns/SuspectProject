using System;
using static SuspectProject.Data.Game;

namespace SuspectProject.Data
{
    [Serializable]
    public class GameUserData : GameDataBase
    {
        // Player's info datas
        public GameDataPrimitive<string> displayName { get; private set; }
        public GameDataPrimitive<string> avatarID { get; private set; }
        public GameDataPrimitive<string> roleID { get; private set; }
        
        // Player's item datas
        public GameDataList<string> equiedItemIDs { get; private set; }
        public GameDataList<string> ownedItemIDs { get; private set; }

        // Player's state datas
        public GameDataPrimitive<float> currentHealth { get; private set; }
        public GameDataPrimitive<float> currentThrist { get; private set; }
        public GameDataPrimitive<float> currentHungry { get; private set; }
        public GameDataPrimitive<float> currentTemperature { get; private set; }
    }
}