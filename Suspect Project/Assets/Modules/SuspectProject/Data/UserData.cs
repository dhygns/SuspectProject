using System;

namespace SuspectProject.Data
{
    [Serializable]
    public class UserData
    {
        public string displayName { get; set; }
        public string avatarID { get; set; }
        public string roleID { get; set; }
    }

}