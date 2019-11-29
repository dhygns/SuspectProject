using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuspectProject.Data
{
    public class SetupPlayerAction : GameActionBase
    {
        public override void Execute(GameStateData state)
        {
            var user = new GameUserData();
            state.users.Add("USER", user);

            user.avatarID.SetValue("AVATAR ID");
            user.displayName.SetValue("DISPLAY ID");
            user.roleID.SetValue("ROLE ID");
        }
    }
}
