using UnityEngine;
using SuspectProject.ObservableState;

namespace SuspectProject
{
    public class ClientState : MonoBehaviour
    {
        public static ClientState instance { get; private set; }

        public StatePrimitiveDictionary<string, Player> players { get; private set; } = new StatePrimitiveDictionary<string, Player>();
        public StatePrimitiveDictionary<string, Item> items { get; private set; } = new StatePrimitiveDictionary<string, Item>();

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }

    public class Player
    {
        public Player()
        {
            avatarID = new StatePrimitive<string>();
            networkID = new StatePrimitive<string>();
            roleID = new StatePrimitive<string>();
            ownedItemIDs = new StatePrimitiveList<string>();

            healthLevel = new StatePrimitive<float>();
            hungryLevel = new StatePrimitive<float>();
            thirstLevel = new StatePrimitive<float>();
            temperature = new StatePrimitive<float>();
        }

        public StatePrimitive<string> avatarID { get; private set; } 
        public StatePrimitive<string> networkID { get; private set; }
        public StatePrimitive<string> roleID { get; private set; }
        public StatePrimitiveList<string> ownedItemIDs { get; private set; }

        public StatePrimitive<float> healthLevel { get; private set; }
        public StatePrimitive<float> hungryLevel { get; private set; }
        public StatePrimitive<float> thirstLevel { get; private set; }
        public StatePrimitive<float> temperature { get; private set; }
    }


    public class Item
    {
        public Item()
        {
            modelID = new StatePrimitive<string>();
        }

        public StatePrimitive<string> modelID { get; private set; } 
    }
}

