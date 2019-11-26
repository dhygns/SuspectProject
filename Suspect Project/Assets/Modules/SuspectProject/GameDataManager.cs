using Photon.Pun;
using System.Collections.Generic;

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
    public class GameDataManager : MonoBehaviourPun, IPunObservable
    {
        public static GameDataManager instance { get; private set; } = null;

        // IObservable, IJsonable
        public Dictionary<string, UserData> users = new Dictionary<string, UserData>();
        public Dictionary<string, ItemData> items = new Dictionary<string, ItemData>();

        public string localUserID = "";
        public UserData localUser => users[localUserID];

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

        public static bool TryGetItemByID(string id, out ItemData itemData)
        {
            return instance.items.TryGetValue(id, out itemData);
        }

        public static bool TryGetUserByID(string id, out UserData userData)
        {
            return instance.users.TryGetValue(id, out userData);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {

        }
    }
}

