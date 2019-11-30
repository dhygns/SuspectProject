using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuspectProject.Data
{
    public class SetupPlayerAction : GameActionBase
    {
        private string _networkID = "";
        private string _avatarID = "";
        private string _displayName = "";
        private string _roleID = "";

        public SetupPlayerAction(string networkID, string displayName)
        {
            _networkID = networkID;

            //TODO : need DB and random avatar ID creator
            _avatarID = "NONE (should be set random avatar ID from DB)";

            _displayName = displayName;

            //TODO : need DB and random role ID creator
            _roleID = "NONE (should be set random role ID from DB)";
        }

        public override void Execute(GameStateData state)
        {
            var user = new GameUserData();
            user.avatarID.SetValue(_avatarID);
            user.displayName.SetValue(_displayName);
            user.roleID.SetValue(_roleID);

            state.users.Add(_networkID, user);
        }
        public override string Description()
        {
            return $"Setup player information\nAvatar ID : {_avatarID}\nDisplay Name : {_displayName}\nRole ID : {_roleID}";
        }
    }
}
