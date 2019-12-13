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
        
        public StateData state { get; private set; }
        public InventoryData inventory { get; private set; }


        // Player's state datas
        [Serializable]
        public class StateData : GameDataBase
        {
            public DataPrimitive<float> health { get; private set; }
            public DataPrimitive<float> maxHealth { get; private set; }

            public DataPrimitive<float> thirst { get; private set; }
            public DataPrimitive<float> maxThirst { get; private set; }

            public DataPrimitive<float> hungry { get; private set; }
            public DataPrimitive<float> maxHungry { get; private set; }

            public DataPrimitive<float> temperature { get; private set; }
        }

        // Player's item datas
        [Serializable]
        public class InventoryData : GameDataBase
        {
            public DataList<string> equiedItemIDs { get; private set; }
            public DataList<string> ownedItemIDs { get; private set; }
        }
        
    }


}