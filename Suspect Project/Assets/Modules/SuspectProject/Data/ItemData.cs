using System;

namespace SuspectProject.Data
{
    [Serializable]
    public class ItemData
    {
        public string itemModelID = "";
        public string ownerID = "";

        public UserData owner => GameDataManager.TryGetUserByID(ownerID, out UserData _owner) ? _owner : null;
    }
}
