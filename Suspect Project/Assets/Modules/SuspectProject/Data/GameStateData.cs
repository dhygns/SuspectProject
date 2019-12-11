using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SuspectProject.Data.Game;

namespace SuspectProject.Data
{
    public class GameStateData : GameDataBase
    {
        public DataDictionary<string, GameUserData> users { get; private set; }
        public DataDictionary<string, GameItemData> items { get; private set; }
    }
}