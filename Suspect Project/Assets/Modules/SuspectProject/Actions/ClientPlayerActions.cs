using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuspectProject.Client
{

    public class ClientPlayerGenerateAction : BaseAction
    {
        private string _networkID;

        public ClientPlayerGenerateAction(string networkID)
        {
            _networkID = networkID;
        }

        public override void Execute(ClientState clientState)
        {
            Player player = clientState.players.AddNew(_networkID);

            player.networkID.value = _networkID;
            //player.avatarID.value = PlayerAvatarManager.GetRandomID();
            //player.roleID.value = PlayerRoleManager.GetRandomID();
        }
    }


    public class ClientPlayerUseItemToTargetAction : BaseAction
    {
        private string _actorPlayerID;
        private string _targetPlayerID;
        private string _itemID;

        public ClientPlayerUseItemToTargetAction(string actorID, string targetID, string itemID)
        {
            _actorPlayerID = actorID;
            _targetPlayerID = targetID;
            _itemID = itemID;
        }

        public override void Execute(ClientState clientState)
        {
            //var targetPlayerState = clientState.players[_targetPlayerID].state;
            //var itemState = clientState.items[_itemID].state;

            //targetPlayerState.healthLevel.value += itemState.healthLevel.value;
            //targetPlayerState.hungryLevel.value += itemState.hungryLevel.value;
            //targetPlayerState.temperature.value += itemState.temperature.value;
            //targetPlayerState.thirstLevel.value += itemState.thirstLevel.value;
        }
    }

}

