using System;

namespace SuspectProject.Data
{
    [Serializable]
    public class GameUserData : GameDataBase
    {
        public GameDataPrimitive<string> displayName { get; private set; }
        public GameDataPrimitive<string> avatarID { get; private set; }
        public GameDataPrimitive<string> roleID { get; private set; }
    }
}