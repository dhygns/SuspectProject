using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SuspectProject.Data.Game;

namespace SuspectProject.Data
{
    public class GameStateData : GameDataBase
    {
        public GameDataDictionary<string, GameUserData> users { get; private set; }
        public GameDataDictionary<string, GameItemData> items { get; private set; }
    }
}