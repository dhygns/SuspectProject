
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkGameInitializer : MonoBehaviourPun
{
    private void Awake()
    {
        PhotonNetwork.Instantiate("TestObject", Vector3.zero, Quaternion.identity, 0);
    }
}
