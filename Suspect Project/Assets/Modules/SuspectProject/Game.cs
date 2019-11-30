
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

        /// <summary>
        /// 메인 게임 상태 데이터입니다. 해당 데이터를 통해 현재 모든 게임의 상태를 관측할 수 있습니다.
        /// </summary>
        public static GameStateData state { get; protected set; } = new GameStateData();

        /// <summary>
        /// 예약된 액션 큐입니다.
        /// </summary>
        public Queue<GameActionBase> reservatedActionQ = new Queue<GameActionBase>();


        /// <summary>
        /// 전체 액션의 히스토리를 기록하는 큐입니다.
        /// </summary>
        public Queue<GameActionBase> historyActionQ = new Queue<GameActionBase>();

        /// <summary>
        /// 최대 저장 가능한 히스토리 갯수입니다.
        /// </summary>
        public int maxHistoryActionCount = 10;


        private static bool _readyToAction = false;

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
                ExecuteAction(new SetupPlayerAction("DEBUG NETWORK ID", "DEBUG DISPLAY NAME"));
            }
        }

        public void LateUpdate()
        {
            while (reservatedActionQ.Count > 0)
            {
                ExecuteAction(reservatedActionQ.Dequeue());
            }
        }

        public static void ScheduleAction(GameActionBase action)
        {
            instance.reservatedActionQ.Enqueue(action);
        }

        public static void ExecuteAction(GameActionBase action)
        {
            // allow changing data value
            _readyToAction = true;

            action.Execute(state);

            // unallow changing data value
            _readyToAction = false;

            // register executed action into history
            instance.historyActionQ.Enqueue(action);

            // keep hisotry count under max histroy count
            while (instance.historyActionQ.Count > instance.maxHistoryActionCount)
                instance.historyActionQ.Dequeue();
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {

        }
    }
}

