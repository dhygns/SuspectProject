using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalDBmanager : MonoBehaviour
{
    #region public types
    #endregion

    public class ItemData
    {
        public enum ItemType
        {
            // 착용가능한 아이템
            Equipable,

            // 일회성 아이템
            Disposable,

            // 섭취가능한 아이템 
            Ingestible
        }

        public string id;
        public string name;
        public string description;

        public ItemType itemType;
    }


    public class RoleData
    {
        [Flags]
        public enum VictoryType
        {
            KillAllPlayersExceptSameRolePlayers = 1,
            KillAllPlayersExceptMe = 2,
            SurviveUntilDueDate = 4,
            CompleteSubMission = 8
        }

        [Flags]
        public enum DefeatType
        {
            DiedAllSameAllianceTypePlayers = 1,
            DiedAllSameRolePlayers = 2,
            FailedSubMission = 4
        }

        public string id;
        public string name;
        public string description;

        public float health;
        public float thirst;
        public float stemina;

        public VictoryType victoryType;
        public DefeatType defeatType;

        // same number, same alliance
        public int allianceNumber;

        // if a target item's level is higher than this, this role can't equip the item.
        public int equipableItemLevel;
    }

    public class AvatarData
    {
        public string resourcePath;
        public string id;
    }

    public static RoleData[] roleDB;
    public AvatarData[] avatarDB;

}
