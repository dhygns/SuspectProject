
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuspectProject.Data
{
    /// <summary>
    /// GameDataManager
    /// 
    /// GameDataManager는 게임 내의 데이터들을 동기화 해주며 저장해줍니다. 
    /// 해당 객체 내에서 관리되는 데이터들은 모든 플레이어 간에 항상 같아야하며
    /// local에는 모든 데이터에 대해 Observer를 등록하여 데이터의 변화를 관찰 할 수 있어야합니다.
    /// 
    /// </summary>
    public partial class Game : MonoBehaviourPun, IPunObservable
    {
        public static Game instance { get; private set; } = null;

        public static GameStateData state { get; protected set; } = new GameStateData();

        public Queue<GameActionBase> exeQ = new Queue<GameActionBase>();

        public Dictionary<GameDataBase, Action> observerDic = new Dictionary<GameDataBase, Action>();
        
        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

        //DEBUG ACTIONS 
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ExecuteAction(new SetupPlayerAction());
                Debug.LogWarning(state);
            }
        }

        public void LateUpdate()
        {
            while (exeQ.Count > 0)
            {
                ExecuteAction(exeQ.Dequeue());
            }
        }

        public static void ScheduleAction(GameActionBase action)
        {
            instance.exeQ.Enqueue(action);
        }

        public static void ExecuteAction(GameActionBase action)
        {
            action.Execute(state);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {

        }
    }
}

