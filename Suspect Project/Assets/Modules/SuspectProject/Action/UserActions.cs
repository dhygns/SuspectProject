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

            user.state.health.SetValue(100.0f);
            user.state.maxHealth.SetValue(100.0f);

            user.state.hungry.SetValue(100.0f);
            user.state.maxHungry.SetValue(100.0f);

            user.state.thirst.SetValue(100.0f);
            user.state.maxThirst.SetValue(100.0f);

            user.state.temperature.SetValue(96.8f);

            state.users.Add(_networkID, user);
        }

        public override string Description()
        {
            return $"Setup player information\nAvatar ID : {_avatarID}\nDisplay Name : {_displayName}\nRole ID : {_roleID}";
        }
    }

    public class GetItemAction : GameActionBase
    {
        private string _userID;
        private string _itemID;

        public GetItemAction(string userID, string itemID)
        {
            _userID = userID;
            _itemID = itemID;
        }

        public override void Execute(GameStateData state)
        {
            state.users[_userID].inventory.ownedItemIDs.Add(_itemID);
        }

        public override string Description()
        {
            return $"user [{_userID}] got item [{_itemID}]";
        }
    }

    public class EquipItemAction : GameActionBase
    {
        private string _userID;
        private string _itemID;

        private bool _isSucceed = false;

        public EquipItemAction(string userID, string itemID)
        {
            _userID = userID;
            _itemID = itemID;
        }

        public override void Execute(GameStateData state)
        {
            if (_isSucceed = state.users[_userID].inventory.ownedItemIDs.Remove(_itemID))
            {
                state.users[_userID].inventory.equiedItemIDs.Add(_itemID);
            }
        }

        public override string Description()
        {
            return $"[{(_isSucceed ? "SUCCEED" : "FAILED") }]user [{_userID}] equip item [{_itemID}]";
        }
    }
}
