using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuspectProject.Data;

namespace SuspectProject
{
    public class Framework : MonoBehaviour
    {
        private static Framework instance = null;

        public string localUserDisplayName = "";

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
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
}

