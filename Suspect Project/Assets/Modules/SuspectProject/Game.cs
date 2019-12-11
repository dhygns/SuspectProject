
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//nametags
using PrimitiveObserver = UnityEngine.Events.UnityAction<System.Collections.Generic.IEnumerable<SuspectProject.Data.Game.DataPrimitive.Action>>;
using DataPrimitiveObservers = System.Collections.Generic.HashSet<UnityEngine.Events.UnityAction<System.Collections.Generic.IEnumerable<SuspectProject.Data.Game.DataPrimitive.Action>>>;
using DataPrimitiveActions = System.Collections.Generic.HashSet<SuspectProject.Data.Game.DataPrimitive.Action>;


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
        /// 옵저버를 동록 / 관리 해줍니다.
        /// </summary>
        public Dictionary<DataPrimitive, DataPrimitiveObservers> observerDic = new Dictionary<DataPrimitive, DataPrimitiveObservers>();

        /// <summary>
        /// 최대 저장 가능한 히스토리 갯수입니다.
        /// </summary>
        public int maxHistoryActionCount = 10;

        private Dictionary<PrimitiveObserver, DataPrimitiveActions> blobs = new Dictionary<PrimitiveObserver, DataPrimitiveActions>();

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
        int debug_index = 0;
        public void Update()
        {

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                RegisterObserver(HandleObservedValueChange, state.users);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                DeregisterObserver(HandleObservedValueChange);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ExecuteAction(new SetupPlayerAction("DEBUG NETWORK ID" + debug_index, "DEBUG DISPLAY NAME" + debug_index));
                debug_index++;
            }
        }

        public void HandleObservedValueChange(IEnumerable<DataPrimitive.Action> actions)
        {
            foreach(var action in actions)
            {
                Debug.LogWarning($"{action.primitive}, {action.type}, {action.parameters}");
            }
        }


        public void LateUpdate()
        {
            // Execute reservated actions
            while (reservatedActionQ.Count > 0)
            {
                ExecuteAction(reservatedActionQ.Dequeue());
            }

            // Check all happend actions and invoke registered listeners
            while (DataPrimitive.ObservedChangedPrimitives.Count > 0)
            {
                var primitiveAction = DataPrimitive.ObservedChangedPrimitives.Dequeue();

                if (observerDic.TryGetValue(primitiveAction.primitive, out DataPrimitiveObservers targetableObservers))
                {

                    foreach (var targetableObserver in targetableObservers)
                    {
                        if (!blobs.TryGetValue(targetableObserver, out DataPrimitiveActions actions))
                        {
                            actions = new DataPrimitiveActions();
                            blobs.Add(targetableObserver, actions);
                        }

                        blobs[targetableObserver].Add(primitiveAction);
                    }
                }
            }

            foreach (var blob in blobs)
            {
                blob.Key?.Invoke(blob.Value);
            }

            blobs.Clear();
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

        public static void RegisterObserver(PrimitiveObserver observer, params DataPrimitive[] targets)
        {
            foreach (var target in targets)
            {
                HashSet<PrimitiveObserver> observers = null;

                if (!instance.observerDic.TryGetValue(target, out observers))
                {
                    observers = new HashSet<PrimitiveObserver>();
                    instance.observerDic.Add(target, observers);
                }

                if (!observers.Contains(observer))
                {
                    observers.Add(observer);
                }
            }
        }

        public static void DeregisterObserver(PrimitiveObserver observer)
        {
            foreach (var observers in instance.observerDic.Values)
            {
                observers.Remove(observer);
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {

        }
        
    }
}

